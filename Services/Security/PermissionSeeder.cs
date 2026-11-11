namespace ZekrHakeem_v11.Services.Security
{
    /// <summary>
    /// مزامنة تعريفات الصلاحيات من الكود إلى قاعدة البيانات + نقل الصلاحيات القديمة
    /// (المخزنة كـ RoleClaims) إلى جدول RolePermissions مرة واحدة.
    /// </summary>
    public static class PermissionSeeder
    {
        private static readonly Dictionary<string, (string Ar, string Color, string Icon, string Desc)> SystemRoles = new()
        {
            [nameof(Roles.Admin)] = ("مدير النظام", "#0F5E4F", "bi-shield-fill-check", "صلاحيات كاملة على كل أجزاء النظام"),
            [nameof(Roles.Supervisor)] = ("مشرف رجال", "#1D7AA8", "bi-person-workspace", "الإشراف على مراكز وحلقات الرجال"),
            [nameof(Roles.SupervisorWomen)] = ("مشرفة نساء", "#B5487E", "bi-person-workspace", "الإشراف على مراكز وحلقات النساء"),
            [nameof(Roles.SupervisorCenterMen)] = ("مشرف مركز رجال", "#2E86AB", "bi-building-check", "الإشراف على مركز رجال محدد"),
            [nameof(Roles.SupervisorCenterWomen)] = ("مشرفة مركز نساء", "#C06C98", "bi-building-check", "الإشراف على مركز نساء محدد"),
            [nameof(Roles.Teacher)] = ("معلم", "#15B392", "bi-person-video3", "معلم حلقات الرجال"),
            [nameof(Roles.TeacherWomen)] = ("معلمة", "#D77FA1", "bi-person-video3", "معلمة حلقات النساء"),
            [nameof(Roles.Parent)] = ("ولي أمر", "#C9A227", "bi-house-heart-fill", "متابعة الأبناء وتقديم الطلبات"),
        };

        public static async Task SyncAsync(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            // 1) Modules + Permissions
            var modules = await db.AppModules.Include(m => m.Permissions).ToListAsync();
            foreach (var def in PermissionCatalog.Modules)
            {
                var module = modules.FirstOrDefault(m => m.Key == def.Key);
                if (module == null)
                {
                    module = new AppModule { Key = def.Key, NameAr = def.NameAr, Icon = def.Icon, SortOrder = def.SortOrder };
                    db.AppModules.Add(module);
                    modules.Add(module);
                }
                foreach (var p in def.Permissions)
                {
                    if (module.Permissions.Any(x => x.Key == p.Key)) continue;
                    module.Permissions.Add(new AppPermission
                    {
                        Key = p.Key, Action = p.Action, NameAr = p.NameAr, SortOrder = p.SortOrder
                    });
                }
            }
            await db.SaveChangesAsync();

            // 2) Role infos
            var roles = await roleManager.Roles.ToListAsync();
            var infos = await db.RoleInfos.ToListAsync();
            foreach (var role in roles)
            {
                if (infos.Any(i => i.RoleId == role.Id)) continue;
                var info = new RoleInfo { RoleId = role.Id };
                if (role.Name != null && SystemRoles.TryGetValue(role.Name, out var meta))
                {
                    info.NameAr = meta.Ar; info.Color = meta.Color; info.Icon = meta.Icon;
                    info.Description = meta.Desc; info.IsSystem = true;
                    info.IsDefaultForRegistration = role.Name == nameof(Roles.Parent);
                }
                db.RoleInfos.Add(info);
            }
            await db.SaveChangesAsync();

            var allPerms = await db.AppPermissions.ToDictionaryAsync(p => p.Key, p => p.Id, StringComparer.OrdinalIgnoreCase);

            // 3) One-time migration of legacy role claims -> RolePermissions
            if (!await db.RolePermissions.AnyAsync())
            {
                var legacy = await db.RoleClaims.Where(c => c.ClaimType == "Permission").ToListAsync();
                foreach (var c in legacy)
                {
                    if (c.ClaimValue != null && allPerms.TryGetValue(c.ClaimValue, out var pid) &&
                        !db.RolePermissions.Local.Any(x => x.RoleId == c.RoleId && x.PermissionId == pid))
                    {
                        db.RolePermissions.Add(new RolePermission { RoleId = c.RoleId, PermissionId = pid, GrantedBy = "migration" });
                    }
                }
                await db.SaveChangesAsync();
                logger.LogInformation("Migrated {Count} legacy permission claims to RolePermissions", legacy.Count);
            }

            // 4) Admin always has every permission
            var admin = roles.FirstOrDefault(r => r.Name == nameof(Roles.Admin));
            if (admin != null)
            {
                var has = await db.RolePermissions.Where(x => x.RoleId == admin.Id).Select(x => x.PermissionId).ToListAsync();
                foreach (var pid in allPerms.Values.Except(has))
                    db.RolePermissions.Add(new RolePermission { RoleId = admin.Id, PermissionId = pid, GrantedBy = "system" });
                await db.SaveChangesAsync();
            }
        }
    }
}
