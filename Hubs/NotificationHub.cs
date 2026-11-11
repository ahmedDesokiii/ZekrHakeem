using Microsoft.AspNetCore.SignalR;

namespace ZekrHakeem_v11.Hubs
{
    /// <summary>
    /// Hub عام لكل المستخدمين المسجلين: تتبع الاتصال (Online) واستقبال الإشعارات اللحظية
    /// مثل تغيير الصلاحيات أو إيقاف الحساب.
    /// </summary>
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly PresenceTracker _presence;
        private readonly IRealtimeNotifier _realtime;

        public NotificationHub(PresenceTracker presence, IRealtimeNotifier realtime)
        {
            _presence = presence;
            _realtime = realtime;
        }

        public override async Task OnConnectedAsync()
        {
            var id = Context.UserIdentifier;
            if (id != null && _presence.Connected(id))
                await _realtime.PresenceChangedAsync(id, true);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = Context.UserIdentifier;
            if (id != null && _presence.Disconnected(id))
                await _realtime.PresenceChangedAsync(id, false);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
