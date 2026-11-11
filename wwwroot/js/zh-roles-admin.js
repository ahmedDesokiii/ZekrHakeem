/* ==========================================================================
   Zekr Hakeem — Roles screen (SignalR)
   ========================================================================== */
(function () {
    'use strict';
    const { esc, toast, confirm, debounce } = ZH;
    const cfg = window.ZH_ROLES || {};
    const $ = (s, r = document) => r.querySelector(s);
    const $$ = (s, r = document) => [...r.querySelectorAll(s)];

    const COLORS = ['#0F5E4F', '#15B392', '#54C392', '#1D7AA8', '#2E86AB', '#5B4BB7', '#B5487E', '#D77FA1', '#C9A227', '#C2571A', '#D64545', '#4A5D5A'];
    const ICONS = ['bi-people-fill', 'bi-shield-fill-check', 'bi-person-workspace', 'bi-person-video3', 'bi-mortarboard-fill', 'bi-building-check', 'bi-house-heart-fill', 'bi-journal-bookmark-fill', 'bi-book-half', 'bi-star-fill', 'bi-award-fill', 'bi-clipboard-data', 'bi-headset', 'bi-gear-fill', 'bi-eye-fill', 'bi-moon-stars-fill'];

    let caps = {}, roles = [], filter = '';

    const { conn, call } = ZH.connectAdminHub(async reconnected => {
        if (!reconnected) { caps = await call('GetMyCapabilities'); ZH.applyCaps(caps); }
        await load();
    });
    const loadSoon = debounce(() => load(), 300);
    conn.on('rolesChanged', loadSoon);
    conn.on('usersChanged', loadSoon);

    async function load() {
        roles = await call('GetRoles');
        render();
    }

    function render() {
        const list = roles.filter(r => !filter || r.display.includes(filter) || r.name.toLowerCase().includes(filter.toLowerCase()));
        const totalUsers = roles.reduce((a, r) => a + r.usersCount, 0);
        $('#rolesSummary').textContent = `${roles.length.toLocaleString('ar-EG')} مجموعة · ${totalUsers.toLocaleString('ar-EG')} عضوية`;
        $('#roles').innerHTML = list.map((r, i) => {
            const pct = r.totalPermissions ? Math.round(r.permissionsCount / r.totalPermissions * 100) : 0;
            const isAdmin = r.name === 'Admin';
            return `<article class="zh-card zh-role-card" style="--c:${esc(r.color)};animation-delay:${i * 30}ms" data-role="${esc(r.id)}">
                <div class="d-flex align-items-start gap-3">
                    <span class="ri"><i class="bi ${esc(r.icon)}"></i></span>
                    <div class="flex-grow-1" style="min-width:0">
                        <h4 class="text-truncate">${esc(r.display)}</h4>
                        <code>${esc(r.name)}</code>
                    </div>
                    <div class="d-flex flex-column align-items-end gap-1">
                        ${r.isSystem ? '<span class="zh-badge zh-badge-muted"><i class="bi bi-lock-fill"></i> نظام</span>' : ''}
                        ${r.isDefaultForRegistration ? '<span class="zh-badge zh-badge-warning"><i class="bi bi-stars"></i> افتراضية</span>' : ''}
                    </div>
                </div>
                <p>${esc(r.description || 'بدون وصف')}</p>
                <div>
                    <div class="d-flex justify-content-between small fw-bold mb-1"><span>الصلاحيات</span><span>${isAdmin ? 'كل الصلاحيات' : `${r.permissionsCount.toLocaleString('ar-EG')} من ${r.totalPermissions.toLocaleString('ar-EG')}`}</span></div>
                    <div class="zh-meter"><span style="width:${isAdmin ? 100 : pct}%"></span></div>
                </div>
                <button type="button" class="d-flex align-items-center gap-2 border-0 bg-transparent p-0 text-start" data-act="members">
                    <span class="zh-avatar-stack">${r.sampleUsers.map(n => `<span class="zh-avatar zh-avatar-sm" style="--a1:${esc(r.color)};--a2:${esc(r.color)}99">${esc(ZH.initials(n))}</span>`).join('')}</span>
                    <span class="fw-bold">${r.usersCount.toLocaleString('ar-EG')} عضو</span><i class="bi bi-chevron-left zh-muted"></i>
                </button>
                <div class="d-flex gap-2 flex-wrap border-top pt-3 mt-auto" style="border-color:var(--zh-line)!important">
                    <a class="zh-btn zh-btn-primary zh-btn-sm flex-grow-1" href="${cfg.permissionsUrl}/${encodeURIComponent(r.id)}"><i class="bi bi-ui-checks-grid"></i> ${caps['Roles.ManagePermissions'] && !isAdmin ? 'إدارة الصلاحيات' : 'عرض الصلاحيات'}</a>
                    ${caps['Roles.Edit'] ? '<button class="zh-btn zh-btn-light zh-btn-sm zh-btn-icon" data-act="edit" title="تعديل"><i class="bi bi-pencil"></i></button>' : ''}
                    ${caps['Roles.Delete'] && !r.isSystem ? '<button class="zh-btn zh-btn-danger zh-btn-sm zh-btn-icon" data-act="delete" title="حذف"><i class="bi bi-trash3"></i></button>' : ''}
                </div>
            </article>`;
        }).join('') || `<div class="zh-card zh-empty" style="grid-column:1/-1"><div class="icon"><i class="bi bi-shield-x"></i></div><h5>لا توجد مجموعات مطابقة</h5></div>`;
    }

    $('#q').addEventListener('input', debounce(e => { filter = e.target.value.trim(); render(); }, 200));
    $('#btnAddRole').onclick = () => openForm();

    $('#roles').addEventListener('click', async e => {
        const card = e.target.closest('[data-role]'); if (!card) return;
        const act = e.target.closest('[data-act]'); if (!act) return;
        const r = roles.find(x => x.id === card.dataset.role);
        if (act.dataset.act === 'edit') openForm(r);
        if (act.dataset.act === 'members') openMembers(r);
        if (act.dataset.act === 'delete') {
            const c = await confirm({ title: 'حذف المجموعة؟', text: `سيتم حذف <b>${esc(r.display)}</b> وكل صلاحياتها.`, danger: true, confirmText: 'حذف' });
            if (!c.isConfirmed) return;
            const res = await call('DeleteRole', r.id);
            res.ok ? toast(res.message) : toast('تعذر الحذف', res.message, 'error', 7000);
        }
    });

    // ---------------------------------------------------------------- members drawer
    async function openMembers(r) {
        $('#membersTitle').textContent = `أعضاء: ${r.display}`;
        $('#membersBody').innerHTML = '<div class="zh-skel" style="height:200px"></div>';
        ZH.openDrawer('membersDrawer');
        const users = await call('GetRoleUsers', r.id);
        $('#membersBody').innerHTML = users.length ? users.map(u => `
            <a class="zh-card d-flex align-items-center gap-3 p-3 mb-2 text-decoration-none text-reset" href="${cfg.usersUrl}?user=${encodeURIComponent(u.id)}">
                ${ZH.avatar(u)}
                <div class="flex-grow-1" style="min-width:0"><b class="d-block text-truncate">${esc(u.fullName)}</b><small class="zh-muted zh-ltr d-block text-end">${esc(u.email || '')}</small></div>
                ${u.isActive ? '' : '<span class="zh-badge zh-badge-danger">موقوف</span>'}
                <i class="bi bi-chevron-left zh-muted"></i>
            </a>`).join('') : `<div class="zh-empty"><div class="icon"><i class="bi bi-people"></i></div><h5>لا يوجد أعضاء</h5></div>`;
    }

    // ---------------------------------------------------------------- role form
    const form = $('#roleForm');
    let color = COLORS[1], icon = ICONS[0];
    $('#colorPick').innerHTML = COLORS.map(c => `<button type="button" data-color="${c}" style="--c:${c}" aria-label="${c}"></button>`).join('');
    $('#iconPick').innerHTML = ICONS.map(i => `<button type="button" data-icon="${i}"><i class="bi ${i}"></i></button>`).join('');
    $('#colorPick').onclick = e => { const b = e.target.closest('[data-color]'); if (b) { color = b.dataset.color; preview(); } };
    $('#iconPick').onclick = e => { const b = e.target.closest('[data-icon]'); if (b) { icon = b.dataset.icon; preview(); } };
    form.nameAr.addEventListener('input', preview);
    form.name.addEventListener('input', preview);

    function preview() {
        $$('#colorPick button').forEach(b => b.classList.toggle('active', b.dataset.color === color));
        $$('#iconPick button').forEach(b => b.classList.toggle('active', b.dataset.icon === icon));
        const p = $('#rolePreview');
        p.style.setProperty('--c', color);
        p.querySelector('.ri').innerHTML = `<i class="bi ${icon}"></i>`;
        p.querySelector('[data-p=name]').textContent = form.nameAr.value || 'اسم المجموعة';
        p.querySelector('[data-p=code]').textContent = form.name.value || 'RoleName';
    }

    function openForm(r) {
        form.reset();
        $('#roleErrors').innerHTML = '';
        form.id.value = r?.id || '';
        form.nameAr.value = r?.nameAr || '';
        form.name.value = r?.name || '';
        form.name.readOnly = !!r?.isSystem;
        form.description.value = r?.description || '';
        form.isDefaultForRegistration.checked = !!r?.isDefaultForRegistration;
        color = r?.color || COLORS[1]; icon = r?.icon || ICONS[0];
        $('#copyFromBlock').hidden = !!r;
        form.copyFromRoleId.innerHTML = '<option value="">— بدون —</option>' + roles.map(x => `<option value="${esc(x.id)}">${esc(x.display)}</option>`).join('');
        $('#roleFormTitle').textContent = r ? `تعديل: ${r.display}` : 'مجموعة جديدة';
        preview();
        ZH.openDrawer('roleDrawer');
        setTimeout(() => form.nameAr.focus(), 350);
    }

    $('#btnSaveRole').onclick = async () => {
        const dto = {
            id: form.id.value || null, name: form.name.value.trim(), nameAr: form.nameAr.value.trim(),
            description: form.description.value.trim(), color, icon,
            isDefaultForRegistration: form.isDefaultForRegistration.checked,
            copyFromRoleId: form.copyFromRoleId.value || null
        };
        if (!dto.nameAr || !dto.name) return toast('الاسم العربي والبرمجي مطلوبان', '', 'warning');
        const res = await call('SaveRole', dto);
        if (res.ok) {
            toast(res.message);
            ZH.closeDrawer('roleDrawer');
            if (!dto.id && caps['Roles.ManagePermissions']) {
                const go = await confirm({ title: 'تم إنشاء المجموعة', text: 'هل تريد تحديد صلاحياتها الآن؟', icon: 'success', confirmText: 'نعم، الصلاحيات' });
                if (go.isConfirmed) location.href = `${cfg.permissionsUrl}/${encodeURIComponent(res.data.id)}`;
            }
        } else {
            $('#roleErrors').innerHTML = `<div class="zh-alert zh-alert-danger mb-3"><i class="bi bi-exclamation-octagon"></i><div>${res.errors.map(esc).join('<br>')}</div></div>`;
        }
    };
})();
