namespace ZekrHakeem_v11.Services.Security
{
    public interface IRoleAdminService
    {
        Task<List<RoleDto>> GetRolesAsync();
        Task<OperationResult> SaveAsync(RoleEditDto dto, string actorId);
        Task<OperationResult> DeleteAsync(string id);
        Task<PermissionMatrixDto?> GetMatrixAsync(string roleId);
        Task<OperationResult> SetPermissionsAsync(string roleId, IEnumerable<int> permissionIds, string actorId);
        Task<List<UserListItemDto>> GetRoleUsersAsync(string roleId);
    }

    public class RoleAdminService : IRoleAdminService
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionService _permissions;
        private readonly IActivityLogger _activity;
        private readonly IRealtimeNotifier _realtime;
        private readonly PresenceTracker _presence;

        public RoleAdminService(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, IPermissionService permissions,
            IActivityLogger activity, IRealtimeNotifier realtime, PresenceTracker presence)
        {
            _db = db; _roleManager = roleManager; _permissions = permissions;
            _activity = activity; _realtime = realtime; _presence = presence;
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var total = await _db.AppPermissions.CountAsync();
            var roles = await (from r in _db.Roles
                               join i in _db.RoleInfos on r.Id equals i.RoleId into gi
                               from i in gi.DefaultIfEmpty()
                               select new RoleDto
                               {
                                   Id = r.Id, Name = r.Name!,
                                   NameAr = i != null ? i.NameAr : null,
                                   Description = i != null ? i.Description : null,
                                   Color = i != null ? i.Color : "#15B392",
                                   Icon = i != null ? i.Icon : "bi-people-fill",
                                   IsSystem = i != null && i.IsSystem,
                                   IsDefaultForRegistration = i != null && i.IsDefaultForRegistration,
                                   UsersCount = _db.UserRoles.Count(ur => ur.RoleId == r.Id),
                                   PermissionsCount = _db.RolePermissions.Count(rp => rp.RoleId == r.Id),
                                   TotalPermissions = total
                               }).ToListAsync();

            var ids = roles.Select(r => r.Id).ToList();
            var samples = await (from ur in _db.UserRoles
                                 join u in _db.Users on ur.UserId equals u.Id
                                 where ids.Contains(ur.RoleId)
                                 select new { ur.RoleId, u.FullName }).ToListAsync();
            foreach (var r in roles)
                r.SampleUsers = samples.Where(s => s.RoleId == r.Id).Select(s => s.FullName).Take(4).ToList();

            return roles.OrderByDescending(r => r.IsSystem).ThenBy(r => r.Display).ToList();
        }

        public async Task<OperationResult> SaveAsync(RoleEditDto dto, string actorId)
        {
            var name = dto.Name?.Trim() ?? "";
            if (name.Length < 2) return OperationResult.Fail("اسم المجموعة (بالإنجليزية) مطلوب");
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, "^[A-Za-z][A-Za-z0-9_]*$"))
                return OperationResult.Fail("اسم المجموعة البرمجي يجب أن يكون بالإنجليزية بدون مسافات (مثال: CenterManager)");

            var isNew = string.IsNullOrEmpty(dto.Id);
            IdentityRole? role = isNew ? new IdentityRole(name) : await _roleManager.FindByIdAsync(dto.Id!);
            if (role == null) return OperationResult.Fail("المجموعة غير موجودة");

            var info = isNew ? null : await _db.RoleInfos.FindAsync(role.Id);
            var dup = await _roleManager.FindByNameAsync(name);
            if (dup != null && dup.Id != role.Id) return OperationResult.Fail("اسم المجموعة موجود من قبل");

            if (!isNew && info?.IsSystem == true && role.Name != name)
                return OperationResult.Fail("لا يمكن تغيير الاسم البرمجي لمجموعات النظام");

            IdentityResult r;
            if (isNew) r = await _roleManager.CreateAsync(role);
            else { role.Name = name; r = await _roleManager.UpdateAsync(role); }
            if (!r.Succeeded) return OperationResult.Fail(r.Errors.Select(UserAdminService.TranslateIdentityError));

            if (info == null) { info = new RoleInfo { RoleId = role.Id }; _db.RoleInfos.Add(info); }
            info.NameAr = string.IsNullOrWhiteSpace(dto.NameAr) ? null : dto.NameAr.Trim();
            info.Description = dto.Description?.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Color)) info.Color = dto.Color;
            if (!string.IsNullOrWhiteSpace(dto.Icon)) info.Icon = dto.Icon;

            if (dto.IsDefaultForRegistration && !info.IsDefaultForRegistration)
            {
                await _db.RoleInfos.Where(x => x.IsDefaultForRegistration && x.RoleId != role.Id)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsDefaultForRegistration, false));
            }
            info.IsDefaultForRegistration = dto.IsDefaultForRegistration;

            if (isNew && !string.IsNullOrEmpty(dto.CopyFromRoleId))
            {
                var src = await _db.RolePermissions.Where(x => x.RoleId == dto.CopyFromRoleId).Select(x => x.PermissionId).ToListAsync();
                _db.RolePermissions.AddRange(src.Select(pid => new RolePermission { RoleId = role.Id, PermissionId = pid, GrantedBy = actorId }));
            }
            await _db.SaveChangesAsync();

            await _activity.LogAsync(isNew ? ActivityActions.RoleCreated : ActivityActions.RoleUpdated, null, info.NameAr ?? name);
            await _realtime.RolesChangedAsync(role.Id, isNew ? "created" : "updated");
            await _realtime.UsersChangedAsync(null, "roles");
            return OperationResult.Success(isNew ? "تم إنشاء المجموعة" : "تم حفظ المجموعة", new { id = role.Id });
        }

        public async Task<OperationResult> DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return OperationResult.Fail("المجموعة غير موجودة");
            var info = await _db.RoleInfos.FindAsync(id);
            if (info?.IsSystem == true) return OperationResult.Fail("لا يمكن حذف مجموعات النظام الأساسية");
            var count = await _db.UserRoles.CountAsync(ur => ur.RoleId == id);
            if (count > 0) return OperationResult.Fail($"لا يمكن حذف المجموعة لوجود {count} مستخدم بها. انقلهم لمجموعة أخرى أولاً");

            var r = await _roleManager.DeleteAsync(role);
            if (!r.Succeeded) return OperationResult.Fail(r.Errors.Select(UserAdminService.TranslateIdentityError));
            await _activity.LogAsync(ActivityActions.RoleDeleted, null, info?.NameAr ?? role.Name);
            await _realtime.RolesChangedAsync(id, "deleted");
            return OperationResult.Success("تم حذف المجموعة");
        }

        public async Task<PermissionMatrixDto?> GetMatrixAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return null;
            var info = await _db.RoleInfos.FindAsync(roleId);
            var granted = (await _db.RolePermissions.Where(x => x.RoleId == roleId).Select(x => x.PermissionId).ToListAsync()).ToHashSet();
            var modules = await _db.AppModules.AsNoTracking().Where(m => m.IsActive).Include(m => m.Permissions).OrderBy(m => m.SortOrder).ToListAsync();
            return new PermissionMatrixDto
            {
                OwnerId = roleId,
                OwnerName = info?.NameAr ?? role.Name!,
                IsSuperAdmin = role.Name == PermissionService.SuperAdminRole,
                Modules = modules.Select(m => new ModuleMatrixDto
                {
                    Id = m.Id, Key = m.Key, NameAr = m.NameAr, Icon = m.Icon ?? "bi-grid",
                    Permissions = m.Permissions.OrderBy(p => p.SortOrder).Select(p => new PermissionCellDto
                    {
                        Id = p.Id, Key = p.Key, Action = p.Action, NameAr = p.NameAr, Granted = granted.Contains(p.Id)
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<OperationResult> SetPermissionsAsync(string roleId, IEnumerable<int> permissionIds, string actorId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return OperationResult.Fail("المجموعة غير موجودة");
            if (role.Name == PermissionService.SuperAdminRole)
                return OperationResult.Fail("مجموعة مدير النظام تملك كل الصلاحيات دائمًا");

            var wanted = (await _db.AppPermissions.Where(p => permissionIds.Contains(p.Id)).Select(p => p.Id).ToListAsync()).ToHashSet();
            var current = await _db.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync();

            _db.RolePermissions.RemoveRange(current.Where(c => !wanted.Contains(c.PermissionId)));
            var have = current.Select(c => c.PermissionId).ToHashSet();
            _db.RolePermissions.AddRange(wanted.Where(w => !have.Contains(w))
                .Select(pid => new RolePermission { RoleId = roleId, PermissionId = pid, GrantedBy = actorId }));
            await _db.SaveChangesAsync();

            _permissions.InvalidateAll();
            var affected = await _db.UserRoles.Where(ur => ur.RoleId == roleId).Select(ur => ur.UserId).ToListAsync();
            await _realtime.UserPermissionsChangedAsync(affected);
            await _activity.LogAsync(ActivityActions.RolePermissionsChanged, null, $"{role.Name}: {wanted.Count} صلاحية");
            await _realtime.RolesChangedAsync(roleId, "permissions");
            return OperationResult.Success($"تم حفظ صلاحيات المجموعة ({wanted.Count} صلاحية) وتطبيقها فورًا على {affected.Count} مستخدم");
        }

        public async Task<List<UserListItemDto>> GetRoleUsersAsync(string roleId)
        {
            var list = await (from ur in _db.UserRoles
                              join u in _db.Users on ur.UserId equals u.Id
                              where ur.RoleId == roleId
                              orderby u.FullName
                              select new UserListItemDto
                              {
                                  Id = u.Id, FullName = u.FullName, UserName = u.UserName, Email = u.Email,
                                  IsActive = u.IsActive, HasPicture = u.ProfilePicture != null, LastLoginAt = u.LastLoginAt
                              }).Take(200).ToListAsync();
            list.ForEach(u => u.IsOnline = _presence.IsOnline(u.Id));
            return list;
        }
    }
}
