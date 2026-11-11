using Microsoft.AspNetCore.SignalR;

namespace ZekrHakeem_v11.Services.Security
{
    /// <summary>إرسال الأحداث اللحظية (SignalR) لشاشات الإدارة وللمستخدمين المتأثرين</summary>
    public interface IRealtimeNotifier
    {
        Task UsersChangedAsync(string? userId, string kind);
        Task RolesChangedAsync(string? roleId, string kind);
        Task PresenceChangedAsync(string userId, bool online);
        Task UserPermissionsChangedAsync(IEnumerable<string> userIds);
        Task ForceLogoutAsync(string userId, string reason);
    }

    public class RealtimeNotifier : IRealtimeNotifier
    {
        private readonly IHubContext<UsersAdminHub> _admin;
        private readonly IHubContext<NotificationHub> _notify;

        public RealtimeNotifier(IHubContext<UsersAdminHub> admin, IHubContext<NotificationHub> notify)
        {
            _admin = admin;
            _notify = notify;
        }

        public Task UsersChangedAsync(string? userId, string kind) =>
            _admin.Clients.All.SendAsync("usersChanged", new { userId, kind });

        public Task RolesChangedAsync(string? roleId, string kind) =>
            _admin.Clients.All.SendAsync("rolesChanged", new { roleId, kind });

        public Task PresenceChangedAsync(string userId, bool online) =>
            _admin.Clients.All.SendAsync("presenceChanged", new { userId, online });

        public Task UserPermissionsChangedAsync(IEnumerable<string> userIds) =>
            _notify.Clients.Users(userIds.Distinct().ToList()).SendAsync("permissionsChanged");

        public Task ForceLogoutAsync(string userId, string reason) =>
            _notify.Clients.User(userId).SendAsync("forceLogout", reason);
    }
}
