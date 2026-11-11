/* ==========================================================================
   Zekr Hakeem — Role permission matrix (SignalR)
   ========================================================================== */
(function () {
    'use strict';
    const { esc, toast, confirm } = ZH;
    const $ = s => document.querySelector(s);
    const roleId = window.ZH_ROLE_ID;
    let caps = {}, matrix = null, data = null, collapsed = false, savingUntil = 0;

    const { conn, call } = ZH.connectAdminHub(async reconnected => {
        if (!reconnected) caps = await call('GetMyCapabilities');
        if (!reconnected || !matrix?.changes().length) await load();
    });

    conn.on('rolesChanged', async ({ roleId: id, kind }) => {
        if (id !== roleId || Date.now() < savingUntil) return;
        if (kind === 'deleted') { toast('تم حذف هذه المجموعة', '', 'warning'); return setTimeout(() => location.href = '/Roles', 1500); }
        if (matrix?.changes().length) return toast('قام مسؤول آخر بتعديل هذه المجموعة', 'احفظ أو تراجع ثم حدّث الصفحة', 'warning', 7000);
        await load();
        toast('تم تحديث الصلاحيات من مسؤول آخر', '', 'info');
    });

    async function load() {
        data = await call('GetRolePermissions', roleId);
        if (!data) { $('#matrix').innerHTML = '<div class="zh-card zh-empty"><div class="icon"><i class="bi bi-shield-x"></i></div><h5>المجموعة غير موجودة</h5></div>'; return; }
        const readOnly = data.isSuperAdmin || !caps['Roles.ManagePermissions'];
        $('#roleName').textContent = data.ownerName;
        document.title = `صلاحيات ${data.ownerName} - الذكر الحكيم`;
        $('#permActions').querySelectorAll('#btnAll,#btnNone').forEach(b => b.hidden = readOnly);
        $('#superAdminNote').innerHTML = data.isSuperAdmin
            ? '<div class="zh-alert zh-alert-success mb-3"><i class="bi bi-shield-fill-check"></i><div>مجموعة <b>مدير النظام</b> تملك كل الصلاحيات دائمًا ولا يمكن تقييدها.</div></div>'
            : (!caps['Roles.ManagePermissions'] ? '<div class="zh-alert zh-alert-info mb-3"><i class="bi bi-eye"></i><div>عرض فقط — ليس لديك صلاحية تعديل صلاحيات المجموعات.</div></div>' : '');
        matrix = ZH.createMatrix($('#matrix'), data, { mode: 'role', readOnly, onChange: onChange });
        summary();
    }

    function summary() {
        const total = data.modules.reduce((a, m) => a + m.permissions.length, 0);
        const on = matrix.grantedIds().length;
        $('#permSummary').textContent = `${on.toLocaleString('ar-EG')} صلاحية مفعلة من ${total.toLocaleString('ar-EG')} في ${data.modules.length.toLocaleString('ar-EG')} وحدة`;
    }

    function onChange(n) {
        summary();
        $('#permChangedCount').textContent = n;
        $('#permSavebar').classList.toggle('show', n > 0);
    }

    $('#permFilter').addEventListener('input', e => matrix && matrix.filter(e.target.value));
    $('#btnAll').onclick = () => matrix.setAll(true);
    $('#btnNone').onclick = () => matrix.setAll(false);
    $('#btnCollapse').onclick = () => { collapsed = !collapsed; matrix.collapseAll(collapsed); $('#btnCollapse').innerHTML = collapsed ? '<i class="bi bi-arrows-expand"></i> فتح' : '<i class="bi bi-arrows-collapse"></i> طي'; };
    $('#permDiscard').onclick = () => matrix.reset();
    $('#permSave').onclick = async () => {
        const btn = $('#permSave'); btn.disabled = true; savingUntil = Date.now() + 5000;
        try {
            const res = await call('SetRolePermissions', roleId, matrix.grantedIds());
            if (res.ok) { toast('تم الحفظ', res.message); matrix.commit(); savingUntil = Date.now() + 1500; }
            else toast('تعذر الحفظ', res.message, 'error');
        } finally { btn.disabled = false; }
    };
    window.addEventListener('beforeunload', e => { if (matrix?.changes().length) { e.preventDefault(); e.returnValue = ''; } });
    document.addEventListener('keydown', e => { if ((e.ctrlKey || e.metaKey) && e.key === 's' && matrix?.changes().length) { e.preventDefault(); $('#permSave').click(); } });
})();
