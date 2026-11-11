/* ==========================================================================
   Zekr Hakeem — shared admin helpers: hub connection, drawers, permission matrix
   ========================================================================== */
(function () {
    'use strict';
    const { esc } = ZH;

    // ------------------------------------------------------------ hub
    function connectAdminHub(onReady) {
        const pill = document.getElementById('liveState');
        const setPill = (on, text) => {
            if (!pill) return;
            pill.classList.toggle('off', !on);
            pill.querySelector('span:last-child').textContent = text;
        };
        const conn = new signalR.HubConnectionBuilder()
            .withUrl('/hubs/users-admin')
            .withAutomaticReconnect([0, 1000, 3000, 5000, 10000, 20000])
            .configureLogging(signalR.LogLevel.Warning)
            .build();

        conn.onreconnecting(() => setPill(false, 'إعادة الاتصال…'));
        conn.onreconnected(() => { setPill(true, 'متصل لحظيًا'); onReady && onReady(true); });
        conn.onclose(() => setPill(false, 'غير متصل'));

        const call = async (method, ...args) => {
            try { return await conn.invoke(method, ...args); }
            catch (err) {
                const msg = (err && err.message || '').replace(/^.*HubException:\s*/, '') || 'حدث خطأ غير متوقع';
                ZH.toast('تعذر تنفيذ العملية', msg, 'error', 6000);
                throw err;
            }
        };

        (async function start() {
            try {
                await conn.start();
                setPill(true, 'متصل لحظيًا');
                onReady && onReady(false);
            } catch (e) {
                setPill(false, 'غير متصل — إعادة المحاولة');
                setTimeout(start, 4000);
            }
        })();
        return { conn, call };
    }

    // ------------------------------------------------------------ capabilities (show/hide by permission)
    function applyCaps(caps) {
        document.querySelectorAll('[data-cap]').forEach(el => {
            const ok = el.dataset.cap.split('|').some(c => caps[c]);
            el.hidden = !ok;
        });
    }

    // ------------------------------------------------------------ drawers
    const backdrop = () => document.getElementById('drawerBackdrop');
    const stack = [];
    function openDrawer(id) {
        const d = document.getElementById(id);
        if (!d) return;
        d.classList.add('open'); d.setAttribute('aria-hidden', 'false');
        backdrop()?.classList.add('open');
        document.body.style.overflow = 'hidden';
        document.body.classList.add('zh-drawer-open');
        if (!stack.includes(id)) stack.push(id);
    }
    function closeDrawer(id) {
        id = id || stack[stack.length - 1];
        const d = document.getElementById(id);
        if (!d) return;
        d.classList.remove('open'); d.setAttribute('aria-hidden', 'true');
        stack.splice(stack.indexOf(id), 1);
        if (!stack.length) { backdrop()?.classList.remove('open'); document.body.style.overflow = ''; document.body.classList.remove('zh-drawer-open'); }
        d.dispatchEvent(new CustomEvent('zh:closed'));
    }
    document.addEventListener('click', e => {
        const c = e.target.closest('[data-close]');
        if (c) closeDrawer(c.closest('.zh-drawer')?.id);
        if (e.target.id === 'drawerBackdrop') closeDrawer();
    });
    document.addEventListener('keydown', e => { if (e.key === 'Escape' && stack.length) closeDrawer(); });

    // ------------------------------------------------------------ permission matrix
    /**
     * mode: 'role'  -> checkbox (granted on/off)
     *       'user'  -> tri-state override cycling: inherit -> grant -> deny -> inherit
     */
    function createMatrix(container, matrix, { mode = 'role', readOnly = false, onChange } = {}) {
        const state = new Map();     // id -> current value (role: bool, user: null/true/false)
        const original = new Map();
        matrix.modules.forEach(m => m.permissions.forEach(p => {
            const v = mode === 'role' ? p.granted : (p.override ?? null);
            state.set(p.id, v); original.set(p.id, v);
        }));

        const effective = p => {
            if (mode === 'role') return state.get(p.id);
            const o = state.get(p.id);
            return o === null ? p.granted : o;
        };

        function cellClass(p) {
            if (mode === 'role') return state.get(p.id) ? 'on' : '';
            const o = state.get(p.id);
            if (o === true) return 'grant';
            if (o === false) return 'deny';
            return p.granted ? 'inherit' : '';
        }
        function cellHint(p) {
            if (mode === 'role') return '';
            const o = state.get(p.id);
            if (o === true) return 'منح مباشر';
            if (o === false) return 'ممنوع مباشرة';
            return p.granted ? 'من: ' + p.sources.join('، ') : 'غير ممنوحة';
        }
        const icon = p => {
            if (mode === 'role') return state.get(p.id) ? '<i class="bi bi-check-lg"></i>' : '';
            const o = state.get(p.id);
            if (o === false) return '<i class="bi bi-x-lg"></i>';
            return effective(p) ? '<i class="bi bi-check-lg"></i>' : '';
        };

        function render(filter = '') {
            const f = filter.trim();
            container.innerHTML = matrix.modules.map(m => {
                const perms = m.permissions.filter(p => !f || p.nameAr.includes(f) || m.nameAr.includes(f) || p.key.toLowerCase().includes(f.toLowerCase()));
                if (!perms.length) return '';
                const on = m.permissions.filter(effective).length;
                const pct = Math.round(on / m.permissions.length * 100);
                return `<div class="zh-card zh-module" data-module="${m.id}">
                    <div class="zh-module-head" data-toggle-module>
                        <span class="mi"><i class="bi ${esc(m.icon)}"></i></span>
                        <div><h5>${esc(m.nameAr)}</h5><small class="zh-muted"><span data-count>${on}</span> من ${m.permissions.length} صلاحية</small></div>
                        <div class="progress-ring">
                            <div class="bar zh-hide-mobile"><span style="width:${pct}%"></span></div>
                            ${readOnly ? '' : `<button type="button" class="zh-btn zh-btn-light zh-btn-sm" data-all="${m.id}">${mode === 'role' ? (on === m.permissions.length ? 'إلغاء الكل' : 'تحديد الكل') : 'إعادة للوراثة'}</button>`}
                            <i class="bi bi-chevron-down chev"></i>
                        </div>
                    </div>
                    <div class="zh-module-body">
                        ${perms.map(p => `<div class="zh-perm ${cellClass(p)} ${state.get(p.id) !== original.get(p.id) ? 'changed' : ''}" data-perm="${p.id}" title="${esc(p.key)}" role="checkbox" tabindex="0">
                            <span class="box">${icon(p)}</span>
                            <span>${esc(p.nameAr)}${mode === 'user' ? `<small>${esc(cellHint(p))}</small>` : ''}</span>
                        </div>`).join('')}
                    </div>
                </div>`;
            }).join('') || `<div class="zh-empty"><div class="icon"><i class="bi bi-search"></i></div><h5>لا توجد نتائج</h5></div>`;
        }

        const find = id => { for (const m of matrix.modules) { const p = m.permissions.find(x => x.id === id); if (p) return p; } };

        function toggle(id) {
            if (readOnly) return;
            const p = find(id); if (!p) return;
            if (mode === 'role') state.set(id, !state.get(id));
            else {
                const o = state.get(id);
                // دورة: وراثة -> (منح لو غير ممنوحة | منع لو ممنوحة) -> العكس -> وراثة
                const next = o === null ? (p.granted ? false : true) : (o === true ? (p.granted ? null : false) : (p.granted ? true : null));
                state.set(id, next);
            }
            refresh();
        }

        let currentFilter = '';
        function refresh() { render(currentFilter); onChange && onChange(changes().length); }

        container.onclick = e => {
            const cell = e.target.closest('[data-perm]');
            if (cell) return toggle(+cell.dataset.perm);
            const all = e.target.closest('[data-all]');
            if (all) {
                e.stopPropagation();
                const m = matrix.modules.find(x => x.id === +all.dataset.all);
                if (mode === 'role') {
                    const allOn = m.permissions.every(p => state.get(p.id));
                    m.permissions.forEach(p => state.set(p.id, !allOn));
                } else m.permissions.forEach(p => state.set(p.id, null));
                return refresh();
            }
            const head = e.target.closest('[data-toggle-module]');
            if (head) head.parentElement.classList.toggle('collapsed');
        };
        container.onkeydown = e => {
            const cell = e.target.closest('[data-perm]');
            if (cell && (e.key === ' ' || e.key === 'Enter')) { e.preventDefault(); toggle(+cell.dataset.perm); }
        };

        function changes() {
            const out = [];
            state.forEach((v, k) => { if (v !== original.get(k)) out.push({ permissionId: k, state: v }); });
            return out;
        }

        render();
        return {
            filter(text) { currentFilter = text; render(text); },
            setAll(value) { if (mode === 'role') { state.forEach((_, k) => state.set(k, value)); refresh(); } },
            changes,
            grantedIds() { return [...state.entries()].filter(([, v]) => v).map(([k]) => k); },
            reset() { original.forEach((v, k) => state.set(k, v)); refresh(); },
            commit() { state.forEach((v, k) => original.set(k, v)); refresh(); },
            collapseAll(c) { container.querySelectorAll('.zh-module').forEach(m => m.classList.toggle('collapsed', c)); }
        };
    }

    Object.assign(window.ZH, { connectAdminHub, applyCaps, openDrawer, closeDrawer, createMatrix });
})();
