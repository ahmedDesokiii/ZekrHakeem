/* ==========================================================================
   Zekr Hakeem — global realtime connection (SignalR NotificationHub)
   - presence (online users)
   - instant permission refresh
   - forced logout when an account is disabled/deleted
   ========================================================================== */
(function () {
    'use strict';
    if (!window.signalR) return;

    const conn = new signalR.HubConnectionBuilder()
        .withUrl('/hubs/notify')
        .withAutomaticReconnect([0, 2000, 5000, 10000, 20000, 30000])
        .configureLogging(signalR.LogLevel.Warning)
        .build();

    const self = document.getElementById('zhSelfPresence');
    const setOnline = on => self && self.classList.toggle('on', on);

    conn.on('permissionsChanged', () => {
        ZH.toast('تم تحديث صلاحياتك', 'سيتم تحديث الصفحة لتطبيق الصلاحيات الجديدة…', 'info', 3500);
        setTimeout(() => location.reload(), 3200);
    });

    conn.on('forceLogout', reason => {
        const go = () => {
            const f = document.getElementById('zhLogoutForm');
            if (f) f.submit(); else location.href = '/Identity/Account/Login';
        };
        if (window.Swal) {
            Swal.fire({ icon: 'warning', title: 'تم إنهاء الجلسة', text: reason || '', confirmButtonText: 'حسنًا', allowOutsideClick: false, buttonsStyling: false, customClass: { confirmButton: 'zh-btn zh-btn-primary' } }).then(go);
            setTimeout(go, 8000);
        } else { alert(reason); go(); }
    });

    conn.onreconnecting(() => setOnline(false));
    conn.onreconnected(() => setOnline(true));
    conn.onclose(() => setOnline(false));

    (async function start() {
        try { await conn.start(); setOnline(true); }
        catch { setOnline(false); setTimeout(start, 5000); }
    })();

    window.ZH = window.ZH || {};
    window.ZH.notifyConnection = conn;
})();
