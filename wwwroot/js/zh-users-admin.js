/* ==========================================================================
   Zekr Hakeem — Users management screen (SignalR + server-side paging)
   ========================================================================== */
(function () {
    'use strict';
    const { esc, avatar, timeAgo, formatDate, toast, confirm, debounce, animateNumber } = ZH;
    const cfg = window.ZH_USERS || {};
    const $ = (s, r = document) => r.querySelector(s);
    const $$ = (s, r = document) => [...r.querySelectorAll(s)];

    let caps = {};
    let roles = [];
    let view = localStorage.getItem('zh.users.view') || 'cards';
    let current = null;          // user in details drawer
    let matrix = null;           // user permissions matrix
    let lastResult = null;
    const query = { search: '', roleId: '', status: '', gender: '', sort: 'name', desc: false, page: 1, pageSize: +(localStorage.getItem('zh.users.ps') || 12) };

    const { conn, call } = ZH.connectAdminHub(async reconnected => {
        if (!reconnected) {
            caps = await call('GetMyCapabilities');
            ZH.applyCaps(caps);
            await loadRoles();
        }
        await Promise.all([loadStats(), load()]);
        if (!reconnected && cfg.openUser) cfg.openUser === 'new' ? openForm() : openUser(cfg.openUser, cfg.openTab);
    });

    // ======================================================== realtime events (from other admins / users)
    let flashId = null;
    const refreshSoon = debounce(async () => {
        await Promise.all([loadStats(), load()]);
        if (flashId) { document.querySelector(`[data-user="${CSS.escape(flashId)}"]`)?.classList.add('zh-flash'); flashId = null; }
    }, 350);
    const statsSoon = debounce(loadStats, 800);

    conn.on('usersChanged', async ({ userId, kind }) => {
        flashId = userId;
        refreshSoon();
        if (current && userId === current.id) {
            if (kind === 'deleted') { ZH.closeDrawer('userDrawer'); return toast('تم حذف هذا المستخدم', '', 'warning'); }
            const editingPerms = $('#permSavebar').classList.contains('show');
            if (!editingPerms && !$('#formDrawer').classList.contains('open')) {
                current = await call('GetUser', userId);
                const active = $('#userTabs .active')?.dataset.tab || 'overview';
                renderUserDrawer(active);
            }
        }
    });
    conn.on('rolesChanged', async () => { await loadRoles(); statsSoon(); });
    conn.on('presenceChanged', ({ userId, online }) => {
        document.querySelectorAll(`[data-presence="${CSS.escape(userId)}"]`).forEach(d => d.classList.toggle('on', online));
        const item = lastResult?.items.find(u => u.id === userId);
        if (item) item.isOnline = online;
        if (current?.id === userId) current.isOnline = online;
        statsSoon();
        if (query.status === 'online') refreshSoon();
    });

    // ======================================================== data loading
    async function loadRoles() {
        roles = await call('GetRoleChips');
        $('#fRole').innerHTML = '<option value="">كل المجموعات</option>' +
            roles.map(r => `<option value="${esc(r.id)}">${esc(r.display)}</option>`).join('') +
            '<option value="none">بدون مجموعة</option>';
        $('#fRole').value = query.roleId;
    }

    async function loadStats() {
        const s = await call('GetStats');
        $$('#stats [data-k]').forEach(el => animateNumber(el, s[el.dataset.k] ?? 0));
        $('#roleFilters').innerHTML = s.byRole.map(r =>
            `<button type="button" class="zh-chip ${query.roleId === r.id ? 'zh-flash' : ''}" style="--c:${esc(r.color)};cursor:pointer;${query.roleId === r.id ? 'outline:2px solid ' + esc(r.color) : ''}" data-role="${esc(r.id)}">
                ${esc(r.display)} <b>${r.count.toLocaleString('ar-EG')}</b></button>`).join('');
    }

    let loadSeq = 0;
    async function load() {
        const seq = ++loadSeq;
        if (!lastResult) renderSkeleton();
        const res = await call('QueryUsers', query);
        if (seq !== loadSeq) return;          // نتيجة قديمة
        lastResult = res;
        render(res);
    }

    // ======================================================== rendering
    function renderSkeleton() {
        $('#results').innerHTML = `<div class="zh-grid">${Array.from({ length: 6 }).map(() => `
            <div class="zh-card zh-user-card"><div class="head"><div class="zh-skel" style="width:52px;height:52px;border-radius:50%"></div>
            <div class="flex-grow-1"><div class="zh-skel mb-2" style="height:16px;width:70%"></div><div class="zh-skel" style="height:12px;width:45%"></div></div></div>
            <div class="zh-skel" style="height:44px"></div><div class="zh-skel" style="height:26px;width:60%"></div></div>`).join('')}</div>`;
    }

    const statusBadge = u => u.isActive
        ? (u.emailConfirmed ? '<span class="zh-badge zh-badge-success"><i class="bi bi-check-circle-fill"></i> مفعل</span>' : '<span class="zh-badge zh-badge-warning"><i class="bi bi-envelope-exclamation"></i> غير مؤكد</span>')
        : '<span class="zh-badge zh-badge-danger"><i class="bi bi-slash-circle"></i> موقوف</span>';
    const roleChips = (list, max = 3) => list.length
        ? list.slice(0, max).map(r => `<span class="zh-chip" style="--c:${esc(r.color)}"><i class="bi ${esc(r.icon)}"></i>${esc(r.display)}</span>`).join('') + (list.length > max ? `<span class="zh-chip">+${list.length - max}</span>` : '')
        : '<span class="zh-badge zh-badge-muted">بدون مجموعة</span>';

    function render(res) {
        const box = $('#results');
        if (!res.items.length) {
            box.innerHTML = `<div class="zh-card zh-empty"><div class="icon"><i class="bi bi-person-x"></i></div>
                <h5>لا يوجد مستخدمين مطابقين</h5><p>جرّب تغيير البحث أو الفلاتر.</p>
                <button class="zh-btn zh-btn-light" id="clearFilters"><i class="bi bi-arrow-counterclockwise"></i> مسح الفلاتر</button></div>`;
        } else if (view === 'table' && innerWidth >= 768) {
            box.innerHTML = `<div class="zh-card zh-table-wrap"><table class="zh-table"><thead><tr>
                <th class="sortable" data-sort="name">المستخدم ${sortIcon('name')}</th>
                <th>التواصل</th><th>المجموعات</th><th>الحالة</th>
                <th class="sortable" data-sort="lastLogin">آخر دخول ${sortIcon('lastLogin')}</th>
                <th class="sortable" data-sort="created">الإضافة ${sortIcon('created')}</th><th></th></tr></thead><tbody>
                ${res.items.map((u, i) => `<tr data-user="${esc(u.id)}" class="${u.isActive ? '' : 'is-inactive'}" style="animation-delay:${i * 18}ms">
                    <td><div class="d-flex align-items-center gap-2">${avatar(u)}<div><b>${esc(u.fullName)}</b><div class="zh-muted small zh-ltr text-end">@${esc(u.userName)}</div></div></div></td>
                    <td><div class="zh-ltr text-end small">${esc(u.email)}</div><div class="zh-muted small zh-ltr text-end">${esc(u.phoneNumber || '')}</div></td>
                    <td><div class="zh-chips">${roleChips(u.roles, 2)}</div></td>
                    <td>${statusBadge(u)}</td>
                    <td class="small">${u.isOnline ? '<span class="zh-badge zh-badge-success"><span class="zh-pulse-dot"></span> متصل الآن</span>' : esc(timeAgo(u.lastLoginAt))}</td>
                    <td class="small zh-muted">${esc(formatDate(u.createdAt))}</td>
                    <td>${rowMenu(u)}</td></tr>`).join('')}
                </tbody></table></div>`;
        } else {
            box.innerHTML = `<div class="zh-grid">${res.items.map((u, i) => `
                <article class="zh-card zh-user-card ${u.isActive ? '' : 'is-inactive'}" data-user="${esc(u.id)}" style="animation-delay:${i * 25}ms">
                    <div class="zh-card-menu">${rowMenu(u)}</div>
                    <div class="head">${avatar(u).replace('--a1', '--s:54px;--a1')}
                        <div class="min-w-0" style="min-width:0"><h3 class="name">${esc(u.fullName)}</h3><div class="sub">@${esc(u.userName)}</div></div>
                    </div>
                    <div class="meta">
                        <span title="${esc(u.email)}"><i class="bi bi-envelope"></i><b class="zh-ltr">${esc(u.email)}</b></span>
                        <span><i class="bi bi-telephone"></i><b class="zh-ltr">${esc(u.phoneNumber || '—')}</b></span>
                        <span><i class="bi bi-person-vcard"></i><b class="zh-ltr">${esc(u.identityNum)}</b></span>
                        <span><i class="bi bi-clock-history"></i>${u.isOnline ? '<b class="text-success">متصل الآن</b>' : esc(timeAgo(u.lastLoginAt))}</span>
                    </div>
                    <div class="zh-chips">${roleChips(u.roles)}</div>
                    <div class="foot">${statusBadge(u)}<span class="zh-muted small">أضيف ${esc(formatDate(u.createdAt))}</span></div>
                </article>`).join('')}</div>`;
        }
        renderPager(res);
    }

    const sortIcon = k => query.sort === k ? `<i class="bi bi-sort-${query.desc ? 'down' : 'up'}"></i>` : '';

    function rowMenu(u) {
        const isMe = u.id === cfg.meId;
        const items = [
            `<li><a class="dropdown-item" href="#" data-act="open">عرض الملف <i class="bi bi-person-lines-fill"></i></a></li>`,
            caps['Users.Edit'] ? `<li><a class="dropdown-item" href="#" data-act="edit">تعديل البيانات <i class="bi bi-pencil-square"></i></a></li>` : '',
            caps['Users.ManagePermissions'] ? `<li><a class="dropdown-item" href="#" data-act="perms">الصلاحيات <i class="bi bi-ui-checks-grid"></i></a></li>` : '',
            caps['Users.ResetPassword'] ? `<li><a class="dropdown-item" href="#" data-act="reset">إعادة تعيين كلمة المرور <i class="bi bi-key"></i></a></li>` : '',
            caps['Users.Lock'] && !isMe ? `<li><a class="dropdown-item" href="#" data-act="toggle">${u.isActive ? 'إيقاف الحساب <i class="bi bi-pause-circle"></i>' : 'تفعيل الحساب <i class="bi bi-play-circle"></i>'}</a></li>` : '',
            caps['Users.Delete'] && !isMe ? `<li><hr class="dropdown-divider"></li><li><a class="dropdown-item text-danger" href="#" data-act="delete">حذف <i class="bi bi-trash3 text-danger"></i></a></li>` : ''
        ].join('');
        return `<div class="dropdown" data-menu><button class="zh-btn zh-btn-ghost zh-btn-icon" data-bs-toggle="dropdown" aria-label="خيارات"><i class="bi bi-three-dots-vertical"></i></button>
            <ul class="dropdown-menu zh-dropdown">${items}</ul></div>`;
    }

    function renderPager(res) {
        const from = res.total ? (res.page - 1) * res.pageSize + 1 : 0;
        const to = Math.min(res.page * res.pageSize, res.total);
        $('#pageInfo').textContent = `عرض ${from.toLocaleString('ar-EG')}–${to.toLocaleString('ar-EG')} من ${res.total.toLocaleString('ar-EG')} مستخدم`;
        const pages = res.pages, p = res.page, btns = [];
        const add = (n, label = n) => btns.push(`<button type="button" data-page="${n}" class="${n === p ? 'active' : ''}">${label}</button>`);
        btns.push(`<button type="button" data-page="${p - 1}" ${p <= 1 ? 'disabled' : ''}><i class="bi bi-chevron-right"></i></button>`);
        for (let i = 1; i <= pages; i++) {
            if (i === 1 || i === pages || Math.abs(i - p) <= 1) add(i);
            else if (btns[btns.length - 1] !== '<button disabled>…</button>') btns.push('<button disabled>…</button>');
        }
        btns.push(`<button type="button" data-page="${p + 1}" ${p >= pages ? 'disabled' : ''}><i class="bi bi-chevron-left"></i></button>`);
        $('#pages').innerHTML = pages > 1 ? btns.join('') : '';
    }

    // ======================================================== toolbar events
    const reload = (resetPage = true) => { if (resetPage) query.page = 1; load(); };
    $('#q').addEventListener('input', debounce(e => { query.search = e.target.value; reload(); }, 280));
    $('#fRole').addEventListener('change', e => { query.roleId = e.target.value; reload(); loadStats(); });
    $('#fGender').addEventListener('change', e => { query.gender = e.target.value; reload(); });
    $('#fSort').addEventListener('change', e => { const [s, d] = e.target.value.split('|'); query.sort = s; query.desc = d === 'desc'; reload(); });
    $('#pageSize').value = String(query.pageSize);
    $('#pageSize').addEventListener('change', e => { query.pageSize = +e.target.value; localStorage.setItem('zh.users.ps', e.target.value); reload(); });
    $('#pages').addEventListener('click', e => { const b = e.target.closest('[data-page]'); if (b && !b.disabled) { query.page = +b.dataset.page; load(); scrollTo({ top: 0, behavior: 'smooth' }); } });
    $$('#viewToggle button').forEach(b => {
        b.classList.toggle('active', b.dataset.view === view);
        b.onclick = () => { view = b.dataset.view; localStorage.setItem('zh.users.view', view); $$('#viewToggle button').forEach(x => x.classList.toggle('active', x === b)); lastResult && render(lastResult); };
    });
    $('#stats').addEventListener('click', e => {
        const s = e.target.closest('.zh-stat'); if (!s) return;
        query.status = s.dataset.status || '';
        if (s.dataset.sort) { query.sort = s.dataset.sort; query.desc = true; $('#fSort').value = 'created|desc'; }
        $$('#stats .zh-stat').forEach(x => x.classList.toggle('is-active', x === s && (!!x.dataset.status || !!x.dataset.sort)));
        reload();
    });
    $('#roleFilters').addEventListener('click', e => {
        const c = e.target.closest('[data-role]'); if (!c) return;
        query.roleId = query.roleId === c.dataset.role ? '' : c.dataset.role;
        $('#fRole').value = query.roleId; reload(); loadStats();
    });
    document.addEventListener('keydown', e => {
        if (e.key === '/' && !/input|textarea|select/i.test(document.activeElement.tagName)) { e.preventDefault(); $('#q').focus(); }
    });
    $('#btnExport').onclick = () => {
        const p = new URLSearchParams({ Search: query.search, RoleId: query.roleId, Status: query.status, Gender: query.gender, Sort: query.sort, Desc: query.desc });
        location.href = cfg.exportUrl + '?' + p.toString();
    };
    $('#btnAddUser').onclick = () => openForm();

    $('#results').addEventListener('click', async e => {
        if (e.target.closest('#clearFilters')) {
            Object.assign(query, { search: '', roleId: '', status: '', gender: '' });
            $('#q').value = ''; $('#fRole').value = ''; $('#fGender').value = '';
            $$('#stats .zh-stat').forEach(x => x.classList.remove('is-active'));
            return reload();
        }
        const act = e.target.closest('[data-act]');
        const host = e.target.closest('[data-user]');
        if (!host) return;
        const id = host.dataset.user;
        if (act) { e.preventDefault(); return runAction(act.dataset.act, id); }
        if (e.target.closest('[data-menu]')) return;
        openUser(id);
    });

    async function runAction(act, id) {
        const u = lastResult?.items.find(x => x.id === id) || (current && current.id === id ? current : { id });
        switch (act) {
            case 'open': return openUser(id);
            case 'edit': return openForm(id);
            case 'perms': return openUser(id, 'perms');
            case 'reset': return resetPassword(u);
            case 'toggle': return toggleActive(u);
            case 'delete': return deleteUser(u);
        }
    }

    // ======================================================== actions
    async function toggleActive(u) {
        const activate = !u.isActive;
        const r = await confirm({
            title: activate ? 'تفعيل الحساب؟' : 'إيقاف الحساب؟',
            text: activate ? `سيتمكن <b>${esc(u.fullName)}</b> من تسجيل الدخول مرة أخرى.` : `سيتم تسجيل خروج <b>${esc(u.fullName)}</b> فورًا من كل الأجهزة ولن يستطيع الدخول.`,
            confirmText: activate ? 'تفعيل' : 'إيقاف', danger: !activate
        });
        if (!r.isConfirmed) return;
        const res = await call('SetUserActive', u.id, activate);
        res.ok ? toast(res.message || 'تم') : toast('تعذر التنفيذ', res.message, 'error');
    }

    async function resetPassword(u) {
        const suggested = await call('GeneratePassword');
        const r = await confirm({
            title: 'إعادة تعيين كلمة المرور', icon: 'question',
            text: `كلمة مرور جديدة لـ <b>${esc(u.fullName || '')}</b> (اتركها كما هي لاستخدام كلمة قوية مولدة):`,
            input: 'text', confirmText: 'تعيين'
        });
        if (!r.isConfirmed) return;
        const res = await call('ResetPassword', u.id, r.value || suggested);
        if (!res.ok) return toast('تعذر التعيين', res.message, 'error', 7000);
        const pw = res.data.password;
        Swal.fire({
            icon: 'success', title: 'تم تعيين كلمة المرور',
            html: `<p class="zh-muted">انسخها وأرسلها للمستخدم — لن تظهر مرة أخرى.</p>
                   <div class="d-flex gap-2 justify-content-center align-items-center"><code class="fs-4 zh-ltr" style="background:#F4EEDD;padding:.4rem .9rem;border-radius:10px">${esc(pw)}</code>
                   <button class="zh-btn zh-btn-light zh-btn-icon" onclick="ZH.copy('${esc(pw)}')"><i class="bi bi-clipboard"></i></button></div>`,
            confirmButtonText: 'تم', buttonsStyling: false, customClass: { confirmButton: 'zh-btn zh-btn-primary' }
        });
    }

    async function deleteUser(u) {
        const r = await confirm({
            title: 'حذف المستخدم نهائيًا؟', danger: true, confirmText: 'حذف',
            text: `سيتم حذف <b>${esc(u.fullName)}</b> نهائيًا. إذا كان مرتبطًا ببيانات يُفضّل إيقاف الحساب بدلاً من الحذف.`
        });
        if (!r.isConfirmed) return;
        const res = await call('DeleteUser', u.id);
        if (res.ok) { toast(res.message); if (current?.id === u.id) ZH.closeDrawer('userDrawer'); }
        else toast('تعذر الحذف', res.message, 'error', 8000);
    }

    // ======================================================== details drawer
    async function openUser(id, tab = 'overview') {
        $('#userDrawerBody').innerHTML = '<div class="zh-skel mb-3" style="height:110px"></div><div class="zh-skel mb-2" style="height:40px"></div><div class="zh-skel" style="height:220px"></div>';
        $('#userDrawerFoot').innerHTML = '';
        ZH.openDrawer('userDrawer');
        const u = await call('GetUser', id);
        if (!u) { ZH.closeDrawer('userDrawer'); return toast('المستخدم غير موجود', '', 'warning'); }
        current = u;
        renderUserDrawer(tab || 'overview');
    }

    function renderUserDrawer(tab) {
        const u = current;
        const tabs = [
            ['overview', 'bi-person', 'نظرة عامة'],
            caps['Users.ManageRoles'] ? ['roles', 'bi-shield-check', 'المجموعات', u.roles.length] : null,
            caps['Users.ManagePermissions'] ? ['perms', 'bi-ui-checks-grid', 'الصلاحيات', u.effectivePermissionsCount] : null,
            caps['Users.ViewActivity'] ? ['activity', 'bi-activity', 'النشاط'] : null
        ].filter(Boolean);
        if (!tabs.find(t => t[0] === tab)) tab = 'overview';

        $('#userDrawerBody').innerHTML = `
            <div class="zh-profile-head">
                ${avatar(u, 'zh-avatar-lg')}
                <div class="flex-grow-1" style="min-width:0">
                    <h4 class="text-truncate">${esc(u.fullName)}</h4>
                    <div class="zh-ltr text-end opacity-75">@${esc(u.userName)}</div>
                    <div class="d-flex flex-wrap gap-2 mt-2">${statusBadge(u)}${u.isOnline ? '<span class="zh-badge zh-badge-success"><span class="zh-pulse-dot"></span> متصل الآن</span>' : ''}${u.twoFactorEnabled ? '<span class="zh-badge zh-badge-muted"><i class="bi bi-shield-lock"></i> 2FA</span>' : ''}</div>
                </div>
            </div>
            <div class="zh-tabs" id="userTabs">${tabs.map(t => `<button type="button" data-tab="${t[0]}" class="${t[0] === tab ? 'active' : ''}"><i class="bi ${t[1]}"></i>${t[2]}${t[3] !== undefined ? `<span class="count">${t[3]}</span>` : ''}</button>`).join('')}</div>
            <div id="userTabBody"></div>`;

        const isMe = u.id === cfg.meId;
        $('#userDrawerFoot').innerHTML = `
            ${caps['Users.Delete'] && !isMe ? '<button class="zh-btn zh-btn-danger me-auto" data-uact="delete"><i class="bi bi-trash3"></i><span class="zh-hide-mobile"> حذف</span></button>' : ''}
            ${caps['Users.Lock'] && !isMe ? `<button class="zh-btn zh-btn-light" data-uact="toggle">${u.isActive ? '<i class="bi bi-pause-circle"></i> إيقاف' : '<i class="bi bi-play-circle"></i> تفعيل'}</button>` : ''}
            ${caps['Users.ResetPassword'] ? '<button class="zh-btn zh-btn-light" data-uact="reset"><i class="bi bi-key"></i><span class="zh-hide-mobile"> كلمة المرور</span></button>' : ''}
            ${caps['Users.Edit'] ? '<button class="zh-btn zh-btn-primary" data-uact="edit"><i class="bi bi-pencil-square"></i> تعديل</button>' : ''}`;

        $('#userTabs').onclick = e => { const b = e.target.closest('[data-tab]'); if (b) renderUserDrawer(b.dataset.tab); };
        $('#userDrawerFoot').onclick = e => { const b = e.target.closest('[data-uact]'); if (b) runAction(b.dataset.uact, u.id); };
        renderTab(tab);
    }

    async function renderTab(tab) {
        const u = current, body = $('#userTabBody');
        hideSavebar();
        if (tab === 'overview') {
            body.innerHTML = `
                <div class="zh-kv mb-3">
                    <div><small>البريد الإلكتروني</small><b class="zh-ltr d-block text-end">${esc(u.email)}</b></div>
                    <div><small>الجوال</small><b class="zh-ltr d-block text-end">${esc(u.phoneNumber || '—')}</b></div>
                    <div><small>رقم الهوية</small><b class="zh-ltr d-block text-end">${esc(u.identityNum)}</b></div>
                    <div><small>النوع</small><b>${u.gender === 'male' ? 'ذكر' : u.gender === 'female' ? 'أنثى' : '—'}</b></div>
                    <div><small>الجنسية</small><b>${esc(u.nationality || '—')}</b></div>
                    <div><small>العنوان</small><b>${esc(u.adress || '—')}</b></div>
                    <div><small>تاريخ الإنشاء</small><b>${esc(formatDate(u.createdAt, true))}</b></div>
                    <div><small>آخر دخول</small><b>${esc(u.lastLoginAt ? formatDate(u.lastLoginAt, true) : 'لم يسجل دخول')}</b></div>
                    <div><small>محاولات دخول فاشلة</small><b>${u.accessFailedCount}</b></div>
                    <div><small>الصلاحيات الفعلية</small><b>${u.effectivePermissionsCount} صلاحية${u.directPermissionsCount ? ` (${u.directPermissionsCount} مباشرة)` : ''}</b></div>
                </div>
                ${u.notes ? `<div class="zh-alert zh-alert-info mb-3"><i class="bi bi-sticky"></i><div>${esc(u.notes)}</div></div>` : ''}
                <h6 class="fw-bold mb-2">المجموعات</h6><div class="zh-chips mb-4">${roleChips(u.roles, 20)}</div>
                ${u.recentActivity?.length ? `<h6 class="fw-bold mb-3">آخر النشاطات</h6>${timeline(u.recentActivity)}` : ''}`;
        } else if (tab === 'roles') {
            const ids = new Set(u.roles.map(r => r.id));
            body.innerHTML = `<p class="zh-muted">اختر المجموعات — الصلاحيات تُطبّق على المستخدم فورًا بعد الحفظ.</p>
                <div class="zh-role-pick mb-3" id="drawerRoles">${rolePicker(ids, 'dr')}</div>
                <button class="zh-btn zh-btn-primary" id="saveRoles"><i class="bi bi-check2-circle"></i> حفظ المجموعات</button>`;
            $('#saveRoles').onclick = async () => {
                const sel = $$('#drawerRoles input:checked').map(i => i.value);
                const res = await call('SetUserRoles', u.id, sel);
                if (res.ok) { toast(res.message); current = await call('GetUser', u.id); renderUserDrawer('roles'); }
            };
        } else if (tab === 'perms') {
            body.innerHTML = '<div class="zh-skel" style="height:300px"></div>';
            const m = await call('GetUserPermissions', u.id);
            if (m.isSuperAdmin) {
                body.innerHTML = `<div class="zh-alert zh-alert-success"><i class="bi bi-shield-fill-check"></i><div>هذا المستخدم <b>مدير للنظام</b> ويملك كل الصلاحيات تلقائيًا.</div></div>`;
                return;
            }
            body.innerHTML = `
                <div class="zh-alert zh-alert-info mb-3"><i class="bi bi-lightbulb"></i><div>الصلاحيات تأتي من المجموعات. اضغط على أي صلاحية لعمل <b>استثناء</b> لهذا المستخدم فقط: منح إضافي أو منع.</div></div>
                <div class="zh-legend mb-3">
                    <span><i style="background:#CDEFE4;border:2px solid #9ADBC6"></i> موروثة من مجموعة</span>
                    <span><i style="background:#1D7AA8"></i> منح مباشر</span>
                    <span><i style="background:#D64545"></i> منع مباشر</span>
                    <span><i style="background:#fff;border:2px solid #C7D3D0"></i> غير ممنوحة</span>
                </div>
                <div class="zh-search mb-3"><i class="bi bi-search"></i><input class="zh-input" id="permFilter" placeholder="ابحث في الصلاحيات…"></div>
                <div class="zh-matrix" id="userMatrix"></div>`;
            matrix = ZH.createMatrix($('#userMatrix'), m, { mode: 'user', onChange: n => n ? showSavebar(n) : hideSavebar() });
            $('#permFilter').oninput = e => matrix.filter(e.target.value);
        } else if (tab === 'activity') {
            body.innerHTML = '<div class="zh-skel" style="height:240px"></div>';
            const res = await call('GetActivity', u.id, 1, 40);
            body.innerHTML = res.items.length ? timeline(res.items) : `<div class="zh-empty"><div class="icon"><i class="bi bi-clock-history"></i></div><h5>لا يوجد نشاط مسجل</h5></div>`;
        }
    }

    function timeline(items) {
        const colors = { Login: '#15B392', LoginFailed: '#D64545', UserDeactivated: '#D64545', UserDeleted: '#D64545', PasswordReset: '#E5A023', RolesChanged: '#1D7AA8', PermissionsChanged: '#1D7AA8' };
        const icons = { Login: 'bi-box-arrow-in-left', LoginFailed: 'bi-shield-exclamation', Logout: 'bi-box-arrow-right', PasswordReset: 'bi-key', RolesChanged: 'bi-shield-check', PermissionsChanged: 'bi-ui-checks-grid', UserCreated: 'bi-person-plus', UserUpdated: 'bi-pencil', UserActivated: 'bi-play-circle', UserDeactivated: 'bi-pause-circle', Register: 'bi-stars', ProfileUpdated: 'bi-person-gear' };
        return `<ul class="zh-timeline">${items.map(a => `<li style="--c:${colors[a.action] || '#15B392'}">
            <span class="ic"><i class="bi ${icons[a.action] || 'bi-dot'}"></i></span>
            <b>${esc(a.actionAr)}</b> ${a.details ? `<span class="zh-muted">— ${esc(a.details)}</span>` : ''}
            <small>${esc(formatDate(a.createdAt, true))}${a.actorName && a.action !== 'Login' ? ` · بواسطة ${esc(a.actorName)}` : ''}${a.ipAddress ? ` · <span class="zh-ltr">${esc(a.ipAddress)}</span>` : ''}</small></li>`).join('')}</ul>`;
    }

    function rolePicker(selectedIds, prefix) {
        return roles.map(r => `<input type="checkbox" id="${prefix}_${esc(r.id)}" value="${esc(r.id)}" ${selectedIds.has(r.id) ? 'checked' : ''}>
            <label for="${prefix}_${esc(r.id)}" style="--c:${esc(r.color)}"><span class="dot"><i class="bi ${esc(r.icon)}"></i></span>${esc(r.display)}<i class="bi bi-check-circle-fill tick"></i></label>`).join('');
    }

    // ------------------------------------------------------- permission savebar
    function showSavebar(n) { $('#permChangedCount').textContent = n; $('#permSavebar').classList.add('show'); }
    function hideSavebar() { $('#permSavebar').classList.remove('show'); }
    $('#permDiscard').onclick = () => matrix && matrix.reset();
    $('#permSave').onclick = async () => {
        if (!matrix || !current) return;
        const res = await call('SetUserPermissions', current.id, matrix.changes());
        if (res.ok) { toast(res.message); matrix.commit(); hideSavebar(); current = await call('GetUser', current.id); }
    };
    $('#userDrawer').addEventListener('zh:closed', () => { hideSavebar(); current = null; matrix = null; });

    // ======================================================== add / edit form
    const form = $('#userForm');
    async function openForm(id) {
        form.reset();
        $('#formErrors').innerHTML = '';
        $$('.zh-input', form).forEach(i => i.classList.remove('is-invalid'));
        form.id.value = '';
        setGender('');
        updateStrength('');
        let u = null;
        if (id) {
            u = current?.id === id ? current : await call('GetUser', id);
            if (!u) return;
            Object.entries({ id: u.id, fullName: u.fullName, userName: u.userName, email: u.email, phoneNumber: u.phoneNumber, identityNum: u.identityNum, nationality: u.nationality, adress: u.adress, notes: u.notes })
                .forEach(([k, v]) => { if (form[k]) form[k].value = v ?? ''; });
            form.isActive.checked = u.isActive;
            setGender(u.gender);
        } else form.isActive.checked = true;

        $('#formTitle').textContent = u ? `تعديل: ${u.fullName}` : 'مستخدم جديد';
        $('#pwLabel').innerHTML = u ? 'كلمة مرور جديدة <span class="zh-muted fw-normal">(اختياري)</span>' : 'كلمة المرور <span class="req">*</span>';
        $('#formRolesBlock').hidden = !caps['Users.ManageRoles'];
        const selected = new Set(u ? u.roles.map(r => r.id) : []);
        $('#formRoles').innerHTML = rolePicker(selected, 'fr');
        ZH.openDrawer('formDrawer');
        setTimeout(() => form.fullName.focus(), 350);
    }

    function setGender(g) {
        form.gender.value = g || '';
        $$('#genderSeg button').forEach(b => b.classList.toggle('active', b.dataset.g === g));
    }
    $('#genderSeg').onclick = e => { const b = e.target.closest('[data-g]'); if (b) setGender(b.dataset.g); };

    form.addEventListener('click', e => {
        const t = e.target.closest('[data-toggle-pw]');
        if (t) { const i = t.parentElement.querySelector('input'); i.type = i.type === 'password' ? 'text' : 'password'; t.innerHTML = `<i class="bi bi-eye${i.type === 'text' ? '-slash' : ''}"></i>`; }
    });
    $('#btnGenPw').onclick = async () => {
        const pw = await call('GeneratePassword');
        form.password.value = pw; form.password.type = 'text'; updateStrength(pw); ZH.copy(pw);
    };
    form.password.addEventListener('input', e => updateStrength(e.target.value));
    function updateStrength(pw) {
        const score = [/[a-z]/, /[A-Z]/, /\d/, /[^A-Za-z0-9]/].filter(r => r.test(pw)).length + (pw.length >= 10 ? 1 : 0);
        const lvl = !pw ? 0 : Math.min(4, Math.max(1, score - (pw.length < 6 ? 2 : 0)));
        const colors = ['#E3E9E7', '#D64545', '#E5A023', '#54C392', '#15B392'];
        $$('#pwStrength span').forEach((s, i) => s.style.background = i < lvl ? colors[lvl] : '#E3E9E7');
    }

    form.addEventListener('submit', e => { e.preventDefault(); save(); });
    $('#btnSaveUser').onclick = save;
    async function save() {
        const dto = {
            id: form.id.value || null,
            fullName: form.fullName.value.trim(), userName: form.userName.value.trim(), email: form.email.value.trim(),
            phoneNumber: form.phoneNumber.value.trim(), identityNum: form.identityNum.value.trim(), gender: form.gender.value,
            nationality: form.nationality.value.trim(), adress: form.adress.value.trim(), notes: form.notes.value.trim(),
            password: form.password.value || null, isActive: form.isActive.checked,
            roleIds: $$('#formRoles input:checked').map(i => i.value)
        };
        // client validation (server validates again)
        const invalid = [];
        [['fullName', dto.fullName.length >= 3], ['userName', /^[A-Za-z0-9._@+-]{3,}$/.test(dto.userName)], ['email', /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(dto.email)], ['identityNum', !!dto.identityNum], ['password', !!dto.id || !!dto.password]]
            .forEach(([k, ok]) => { form[k].classList.toggle('is-invalid', !ok); if (!ok) invalid.push(k); });
        if (invalid.length) { form[invalid[0]].focus(); return toast('راجع الحقول المطلوبة', '', 'warning'); }

        const btn = $('#btnSaveUser'); btn.disabled = true; btn.innerHTML = '<span class="spinner-border spinner-border-sm"></span> جارٍ الحفظ…';
        try {
            const res = await call('SaveUser', dto);
            if (res.ok) {
                toast(res.message);
                ZH.closeDrawer('formDrawer');
                if (current && current.id === dto.id) { current = await call('GetUser', dto.id); renderUserDrawer('overview'); }
            } else {
                $('#formErrors').innerHTML = `<div class="zh-alert zh-alert-danger mb-3"><i class="bi bi-exclamation-octagon"></i><ul>${res.errors.map(x => `<li>${esc(x)}</li>`).join('')}</ul></div>`;
                $('#formDrawer .zh-drawer-body').scrollTop = 0;
            }
        } finally { btn.disabled = false; btn.innerHTML = '<i class="bi bi-check2-circle"></i> حفظ'; }
    }
})();
