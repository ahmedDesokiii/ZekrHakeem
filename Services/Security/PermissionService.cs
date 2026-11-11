using Microsoft.Extensions.Caching.Memory;

namespace ZekrHakeem_v11.Services.Security
{
    public interface IPermissionService
    {
        Task<IReadOnlySet<string>> GetUserPermissionsAsync(string userId);
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission);
        void Invalidate(string userId);
        void InvalidateAll();
    }

    /// <summary>
    /// يحسب الصلاحيات الفعلية للمستخدم من قاعدة البيانات:
    /// (صلاحيات كل مجموعاته) + (المنح المباشر) - (المنع المباشر)
    /// مع كاش في الذاكرة يتم مسحه فور أي تعديل.
    /// </summary>
    public class PermissionService : IPermissionService
    {
        public const string SuperAdminRole = nameof(Constants.Roles.Admin);
        private static int _version;

        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;

        public PermissionService(ApplicationDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        private static string Key(string userId) => $"perm:{Volatile.Read(ref _version)}:{userId}";

        public async Task<IReadOnlySet<string>> GetUserPermissionsAsync(string userId)
        {
            if (_cache.TryGetValue(Key(userId), out IReadOnlySet<string>? cached) && cached != null)
                return cached;

            var roleIds = _db.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId);

            var fromRoles = await _db.RolePermissions
                .Where(rp => roleIds.Contains(rp.RoleId))
                .Select(rp => rp.Permission!.Key)
                .Distinct()
                .ToListAsync();

            var overrides = await _db.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => new { up.Permission!.Key, up.IsGranted })
                .ToListAsync();

            var set = new HashSet<string>(fromRoles, StringComparer.OrdinalIgnoreCase);
            foreach (var o in overrides)
            {
                if (o.IsGranted) set.Add(o.Key); else set.Remove(o.Key);
            }

            _cache.Set(Key(userId), (IReadOnlySet<string>)set, TimeSpan.FromMinutes(10));
            return set;
        }

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission)
        {
            if (user?.Identity?.IsAuthenticated != true) return false;
            if (user.IsInRole(SuperAdminRole)) return true;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return false;

            var perms = await GetUserPermissionsAsync(userId);
            return perms.Contains(permission);
        }

        public void Invalidate(string userId) => _cache.Remove(Key(userId));
        public void InvalidateAll() => Interlocked.Increment(ref _version);
    }
}
