using System.Security.Cryptography;

namespace ZekrHakeem_v11.Services.Security
{
    public interface IUserAdminService
    {
        Task<UsersStatsDto> GetStatsAsync();
        Task<PagedResult<UserListItemDto>> QueryAsync(UserQuery q);
        Task<UserDetailsDto?> GetDetailsAsync(string id);
        Task<OperationResult> SaveAsync(UserEditDto dto, string actorId);
        Task<OperationResult> SetActiveAsync(string id, bool active, string actorId);
        Task<OperationResult> ResetPasswordAsync(string id, string? newPassword);
        Task<OperationResult> SetRolesAsync(string id, IEnumerable<string> roleIds, string actorId);
        Task<PermissionMatrixDto?> GetPermissionMatrixAsync(string id);
        Task<OperationResult> SetPermissionOverridesAsync(string id, IEnumerable<UserPermissionOverrideDto> overrides, string actorId);
        Task<OperationResult> DeleteAsync(string id, string actorId);
        Task<PagedResult<ActivityDto>> GetActivityAsync(string? userId, int page, int pageSize);
        Task<List<RoleChipDto>> GetRoleChipsAsync();
    }

    public class UserAdminService : IUserAdminService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionService _permissions;
        private readonly IActivityLogger _activity;
        private readonly IRealtimeNotifier _realtime;
        private readonly PresenceTracker _presence;

        public UserAdminService(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IPermissionService permissions, IActivityLogger activity,
            IRealtimeNotifier realtime, PresenceTracker presence)
        {
            _db = db; _userManager = userManager; _roleManager = roleManager;
            _permissions = permissions; _activity = activity; _realtime = realtime; _presence = presence;
        }

        // ---------------------------------------------------------------- lookups
        public async Task<List<RoleChipDto>> GetRoleChipsAsync()
        {
            return await (from r in _db.Roles
                          join i in _db.RoleInfos on r.Id equals i.RoleId into gi
                          from i in gi.DefaultIfEmpty()
                          orderby r.Name
                          select new RoleChipDto
                          {
                              Id = r.Id,
                              Name = r.Name!,
                              Display = i != null && i.NameAr != null ? i.NameAr : r.Name!,
                              Color = i != null ? i.Color : "#15B392",
                              Icon = i != null ? i.Icon : "bi-people-fill"
                          }).ToListAsync();
        }

        // ---------------------------------------------------------------- stats
        public async Task<UsersStatsDto> GetStatsAsync()
        {
            var monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var stats = new UsersStatsDto
            {
                Total = await _db.Users.CountAsync(),
                Active = await _db.Users.CountAsync(u => u.IsActive),
                Inactive = await _db.Users.CountAsync(u => !u.IsActive),
                Unconfirmed = await _db.Users.CountAsync(u => !u.EmailConfirmed),
                NewThisMonth = await _db.Users.CountAsync(u => u.CreatedAt >= monthStart),
                Online = _presence.OnlineCount
            };
            var chips = await GetRoleChipsAsync();
            var counts = await _db.UserRoles.GroupBy(ur => ur.RoleId)
                .Select(g => new { g.Key, Count = g.Count() }).ToDictionaryAsync(x => x.Key, x => x.Count);
            stats.ByRole = chips.Select(c => new RoleCountDto
            {
                Id = c.Id, Display = c.Display, Color = c.Color, Count = counts.TryGetValue(c.Id, out var n) ? n : 0
            }).OrderByDescending(x => x.Count).ToList();
            return stats;
        }

        // ---------------------------------------------------------------- query (server side paging)
        public async Task<PagedResult<UserListItemDto>> QueryAsync(UserQuery q)
        {
            q.Page = Math.Max(1, q.Page);
            q.PageSize = Math.Clamp(q.PageSize, 5, 100);

            IQueryable<ApplicationUser> users = _db.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(q.Search))
            {
                var s = q.Search.Trim();
                users = users.Where(u => u.FullName.Contains(s) || u.UserName!.Contains(s) || u.Email!.Contains(s)
                                      || u.PhoneNumber!.Contains(s) || u.IdentityNum.Contains(s));
            }
            if (!string.IsNullOrWhiteSpace(q.RoleId))
            {
                if (q.RoleId == "none")
                    users = users.Where(u => !_db.UserRoles.Any(ur => ur.UserId == u.Id));
                else
                    users = users.Where(u => _db.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == q.RoleId));
            }
            if (!string.IsNullOrWhiteSpace(q.Gender))
                users = users.Where(u => u.Gender == q.Gender);

            switch (q.Status)
            {
                case "active": users = users.Where(u => u.IsActive); break;
                case "inactive": users = users.Where(u => !u.IsActive); break;
                case "unconfirmed": users = users.Where(u => !u.EmailConfirmed); break;
                case "online":
                    var online = _presence.OnlineUserIds.ToList();
                    users = users.Where(u => online.Contains(u.Id));
                    break;
            }

            users = (q.Sort, q.Desc) switch
            {
                ("username", false) => users.OrderBy(u => u.UserName),
                ("username", true) => users.OrderByDescending(u => u.UserName),
                ("created", false) => users.OrderBy(u => u.CreatedAt),
                ("created", true) => users.OrderByDescending(u => u.CreatedAt),
                ("lastLogin", false) => users.OrderBy(u => u.LastLoginAt),
                ("lastLogin", true) => users.OrderByDescending(u => u.LastLoginAt),
                (_, true) => users.OrderByDescending(u => u.FullName),
                _ => users.OrderBy(u => u.FullName)
            };

            var total = await users.CountAsync();
            var page = await users.Skip((q.Page - 1) * q.PageSize).Take(q.PageSize)
                .Select(u => new UserListItemDto
                {
                    Id = u.Id, FullName = u.FullName, UserName = u.UserName, Email = u.Email,
                    PhoneNumber = u.PhoneNumber, IdentityNum = u.IdentityNum, Gender = u.Gender,
                    IsActive = u.IsActive, EmailConfirmed = u.EmailConfirmed,
                    HasPicture = u.ProfilePicture != null,
                    CreatedAt = u.CreatedAt, LastLoginAt = u.LastLoginAt
                }).ToListAsync();

            await FillRolesAsync(page);
            foreach (var u in page) u.IsOnline = _presence.IsOnline(u.Id);

            return new PagedResult<UserListItemDto> { Items = page, Total = total, Page = q.Page, PageSize = q.PageSize };
        }

        private async Task FillRolesAsync(IEnumerable<UserListItemDto> users)
        {
            var ids = users.Select(u => u.Id).ToList();
            if (ids.Count == 0) return;
            var chips = (await GetRoleChipsAsync()).ToDictionary(c => c.Id);
            var links = await _db.UserRoles.Where(ur => ids.Contains(ur.UserId)).ToListAsync();
            foreach (var u in users)
                u.Roles = links.Where(l => l.UserId == u.Id && chips.ContainsKey(l.RoleId))
                               .Select(l => chips[l.RoleId]).OrderBy(c => c.Display).ToList();
        }

        // ---------------------------------------------------------------- details
        public async Task<UserDetailsDto?> GetDetailsAsync(string id)
        {
            var u = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (u == null) return null;
            var d = new UserDetailsDto
            {
                Id = u.Id, FullName = u.FullName, UserName = u.UserName, Email = u.Email, PhoneNumber = u.PhoneNumber,
                IdentityNum = u.IdentityNum, Gender = u.Gender, IsActive = u.IsActive, EmailConfirmed = u.EmailConfirmed,
                HasPicture = u.ProfilePicture != null, CreatedAt = u.CreatedAt, LastLoginAt = u.LastLoginAt,
                Nationality = u.Nationality, Adress = u.Adress, Notes = u.Notes, TwoFactorEnabled = u.TwoFactorEnabled,
                AccessFailedCount = u.AccessFailedCount, IsOnline = _presence.IsOnline(u.Id)
            };
            await FillRolesAsync(new[] { d });
            d.DirectPermissionsCount = await _db.UserPermissions.CountAsync(x => x.UserId == id);
            d.EffectivePermissionsCount = d.Roles.Any(r => r.Name == PermissionService.SuperAdminRole)
                ? await _db.AppPermissions.CountAsync()
                : (await _permissions.GetUserPermissionsAsync(id)).Count;
            d.RecentActivity = (await GetActivityAsync(id, 1, 8)).Items;
            return d;
        }

        // ---------------------------------------------------------------- create / update
        public async Task<OperationResult> SaveAsync(UserEditDto dto, string actorId)
        {
            var errors = Validate(dto);
            if (errors.Count > 0) return OperationResult.Fail(errors);

            dto.UserName = dto.UserName.Trim();
            dto.Email = dto.Email.Trim();
            dto.IdentityNum = dto.IdentityNum.Trim();

            var isNew = string.IsNullOrEmpty(dto.Id);
            ApplicationUser? user = isNew ? new ApplicationUser() : await _userManager.FindByIdAsync(dto.Id!);
            if (user == null) return OperationResult.Fail("المستخدم غير موجود");

            // تكرار
            if (await _db.Users.AnyAsync(u => u.Id != user.Id && u.NormalizedUserName == dto.UserName.ToUpper()))
                return OperationResult.Fail("اسم المستخدم مستخدم من قبل");
            if (await _db.Users.AnyAsync(u => u.Id != user.Id && u.NormalizedEmail == dto.Email.ToUpper()))
                return OperationResult.Fail("البريد الإلكتروني مستخدم من قبل");
            if (await _db.Users.AnyAsync(u => u.Id != user.Id && u.IdentityNum == dto.IdentityNum))
                return OperationResult.Fail("رقم الهوية مسجل لمستخدم آخر");

            if (!isNew && user.Id == actorId && !dto.IsActive)
                return OperationResult.Fail("لا يمكنك إيقاف حسابك الحالي");

            user.FullName = dto.FullName.Trim();
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber?.Trim();
            user.IdentityNum = dto.IdentityNum;
            user.Gender = dto.Gender ?? string.Empty;
            user.Nationality = dto.Nationality;
            user.Adress = dto.Adress;
            user.Notes = dto.Notes;
            var wasActive = user.IsActive;
            user.IsActive = dto.IsActive;

            IdentityResult result;
            if (isNew)
            {
                user.Id = Guid.NewGuid().ToString();
                user.EmailConfirmed = true; // المستخدم المضاف من الإدارة مؤكد
                user.CreatedAt = DateTime.UtcNow;
                result = await _userManager.CreateAsync(user, dto.Password!);
            }
            else
            {
                result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrWhiteSpace(dto.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    result = await _userManager.ResetPasswordAsync(user, token, dto.Password);
                }
            }
            if (!result.Succeeded) return OperationResult.Fail(result.Errors.Select(TranslateIdentityError));

            await ApplyRolesAsync(user, dto.RoleIds, actorId);

            if (!isNew && wasActive && !user.IsActive)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _realtime.ForceLogoutAsync(user.Id, "تم إيقاف حسابك من قبل الإدارة");
            }

            await _activity.LogAsync(isNew ? ActivityActions.UserCreated : ActivityActions.UserUpdated, user.Id, user.FullName);
            await _realtime.UsersChangedAsync(user.Id, isNew ? "created" : "updated");
            return OperationResult.Success(isNew ? "تم إضافة المستخدم بنجاح" : "تم حفظ التعديلات بنجاح", new { id = user.Id });
        }

        private static List<string> Validate(UserEditDto dto)
        {
            var e = new List<string>();
            if (string.IsNullOrWhiteSpace(dto.FullName) || dto.FullName.Trim().Length < 3) e.Add("الاسم الكامل مطلوب (3 حروف على الأقل)");
            if (string.IsNullOrWhiteSpace(dto.UserName) || dto.UserName.Trim().Length < 3) e.Add("اسم المستخدم مطلوب (3 حروف على الأقل)");
            if (string.IsNullOrWhiteSpace(dto.Email) || !new EmailAddressAttribute().IsValid(dto.Email.Trim())) e.Add("البريد الإلكتروني غير صحيح");
            if (string.IsNullOrWhiteSpace(dto.IdentityNum)) e.Add("رقم الهوية مطلوب");
            if (string.IsNullOrEmpty(dto.Id) && string.IsNullOrWhiteSpace(dto.Password)) e.Add("كلمة المرور مطلوبة للمستخدم الجديد");
            return e;
        }

        private async Task<bool> ApplyRolesAsync(ApplicationUser user, IEnumerable<string> roleIds, string actorId)
        {
            var wanted = await _db.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.Name!).ToListAsync();
            var current = await _userManager.GetRolesAsync(user);

            // حماية: لا يمكن للمسؤول إزالة دور المدير من نفسه
            if (user.Id == actorId && current.Contains(PermissionService.SuperAdminRole) && !wanted.Contains(PermissionService.SuperAdminRole))
                wanted.Add(PermissionService.SuperAdminRole);

            var toRemove = current.Except(wanted).ToList();
            var toAdd = wanted.Except(current).ToList();
            if (toRemove.Count == 0 && toAdd.Count == 0) return false;

            if (toRemove.Count > 0) await _userManager.RemoveFromRolesAsync(user, toRemove);
            if (toAdd.Count > 0) await _userManager.AddToRolesAsync(user, toAdd);

            // الـ SecurityStampValidator (ValidationInterval = 0) يعيد بناء الـ Claims في الطلب التالي تلقائيًا
            _permissions.Invalidate(user.Id);
            await _realtime.UserPermissionsChangedAsync(new[] { user.Id });
            return true;
        }

        public async Task<OperationResult> SetRolesAsync(string id, IEnumerable<string> roleIds, string actorId)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return OperationResult.Fail("المستخدم غير موجود");
            var changed = await ApplyRolesAsync(user, roleIds, actorId);
            if (changed)
            {
                var names = string.Join("، ", (await GetRoleChipsAsync()).Where(c => roleIds.Contains(c.Id)).Select(c => c.Display));
                await _activity.LogAsync(ActivityActions.RolesChanged, id, names);
                await _realtime.UsersChangedAsync(id, "roles");
            }
            return OperationResult.Success("تم تحديث المجموعات");
        }

        // ---------------------------------------------------------------- activate / password
        public async Task<OperationResult> SetActiveAsync(string id, bool active, string actorId)
        {
            if (id == actorId && !active) return OperationResult.Fail("لا يمكنك إيقاف حسابك الحالي");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return OperationResult.Fail("المستخدم غير موجود");
            if (user.IsActive == active) return OperationResult.Success();

            user.IsActive = active;
            if (active) { user.LockoutEnd = null; user.AccessFailedCount = 0; }
            await _userManager.UpdateAsync(user);
            if (!active)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _realtime.ForceLogoutAsync(user.Id, "تم إيقاف حسابك من قبل الإدارة");
            }
            await _activity.LogAsync(active ? ActivityActions.UserActivated : ActivityActions.UserDeactivated, id, user.FullName);
            await _realtime.UsersChangedAsync(id, "status");
            return OperationResult.Success(active ? "تم تفعيل الحساب" : "تم إيقاف الحساب وتسجيل خروجه من كل الأجهزة");
        }

        public async Task<OperationResult> ResetPasswordAsync(string id, string? newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return OperationResult.Fail("المستخدم غير موجود");
            var password = string.IsNullOrWhiteSpace(newPassword) ? GeneratePassword() : newPassword;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var r = await _userManager.ResetPasswordAsync(user, token, password);
            if (!r.Succeeded) return OperationResult.Fail(r.Errors.Select(TranslateIdentityError));
            await _userManager.UpdateSecurityStampAsync(user);
            await _activity.LogAsync(ActivityActions.PasswordReset, id, user.FullName);
            return OperationResult.Success("تم تعيين كلمة مرور جديدة", new { password });
        }

        public static string GeneratePassword()
        {
            const string upper = "ABCDEFGHJKLMNPQRSTUVWXYZ", lower = "abcdefghijkmnpqrstuvwxyz", digits = "23456789", sym = "#$@!%&*";
            var all = upper + lower + digits + sym;
            var chars = new List<char>
            {
                upper[RandomNumberGenerator.GetInt32(upper.Length)], lower[RandomNumberGenerator.GetInt32(lower.Length)],
                digits[RandomNumberGenerator.GetInt32(digits.Length)], sym[RandomNumberGenerator.GetInt32(sym.Length)]
            };
            while (chars.Count < 10) chars.Add(all[RandomNumberGenerator.GetInt32(all.Length)]);
            return new string(chars.OrderBy(_ => RandomNumberGenerator.GetInt32(1000)).ToArray());
        }

        // ---------------------------------------------------------------- direct permissions
        public async Task<PermissionMatrixDto?> GetPermissionMatrixAsync(string id)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            var roleIds = await _db.UserRoles.Where(ur => ur.UserId == id).Select(ur => ur.RoleId).ToListAsync();
            var chips = (await GetRoleChipsAsync()).ToDictionary(c => c.Id);
            var rolePerms = await _db.RolePermissions.Where(rp => roleIds.Contains(rp.RoleId)).ToListAsync();
            var overrides = await _db.UserPermissions.Where(up => up.UserId == id).ToDictionaryAsync(x => x.PermissionId, x => x.IsGranted);
            var modules = await _db.AppModules.AsNoTracking().Where(m => m.IsActive).Include(m => m.Permissions).OrderBy(m => m.SortOrder).ToListAsync();

            return new PermissionMatrixDto
            {
                OwnerId = id,
                OwnerName = user.FullName,
                IsSuperAdmin = roleIds.Any(r => chips.TryGetValue(r, out var c) && c.Name == PermissionService.SuperAdminRole),
                Modules = modules.Select(m => new ModuleMatrixDto
                {
                    Id = m.Id, Key = m.Key, NameAr = m.NameAr, Icon = m.Icon ?? "bi-grid",
                    Permissions = m.Permissions.OrderBy(p => p.SortOrder).Select(p =>
                    {
                        var sources = rolePerms.Where(rp => rp.PermissionId == p.Id && chips.ContainsKey(rp.RoleId))
                                               .Select(rp => chips[rp.RoleId].Display).ToList();
                        return new PermissionCellDto
                        {
                            Id = p.Id, Key = p.Key, Action = p.Action, NameAr = p.NameAr,
                            Granted = sources.Count > 0, Sources = sources,
                            Override = overrides.TryGetValue(p.Id, out var o) ? o : null
                        };
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<OperationResult> SetPermissionOverridesAsync(string id, IEnumerable<UserPermissionOverrideDto> overrides, string actorId)
        {
            if (!await _db.Users.AnyAsync(u => u.Id == id)) return OperationResult.Fail("المستخدم غير موجود");
            var existing = await _db.UserPermissions.Where(x => x.UserId == id).ToListAsync();
            var validIds = await _db.AppPermissions.Select(p => p.Id).ToListAsync();

            foreach (var o in overrides.Where(o => validIds.Contains(o.PermissionId)))
            {
                var row = existing.FirstOrDefault(x => x.PermissionId == o.PermissionId);
                if (o.State == null)
                {
                    if (row != null) _db.UserPermissions.Remove(row);
                }
                else if (row == null)
                {
                    _db.UserPermissions.Add(new UserPermission { UserId = id, PermissionId = o.PermissionId, IsGranted = o.State.Value, GrantedBy = actorId });
                }
                else
                {
                    row.IsGranted = o.State.Value; row.GrantedAt = DateTime.UtcNow; row.GrantedBy = actorId;
                }
            }
            await _db.SaveChangesAsync();
            _permissions.Invalidate(id);
            await _activity.LogAsync(ActivityActions.PermissionsChanged, id, $"{overrides.Count()} تعديل");
            await _realtime.UserPermissionsChangedAsync(new[] { id });
            await _realtime.UsersChangedAsync(id, "permissions");
            return OperationResult.Success("تم حفظ الصلاحيات المباشرة");
        }

        // ---------------------------------------------------------------- delete
        public async Task<OperationResult> DeleteAsync(string id, string actorId)
        {
            if (id == actorId) return OperationResult.Fail("لا يمكنك حذف حسابك الحالي");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return OperationResult.Fail("المستخدم غير موجود");
            if (await _userManager.IsInRoleAsync(user, PermissionService.SuperAdminRole) &&
                (await _userManager.GetUsersInRoleAsync(PermissionService.SuperAdminRole)).Count <= 1)
                return OperationResult.Fail("لا يمكن حذف آخر مدير للنظام");

            try
            {
                var r = await _userManager.DeleteAsync(user);
                if (!r.Succeeded) return OperationResult.Fail(r.Errors.Select(TranslateIdentityError));
            }
            catch (DbUpdateException)
            {
                _db.ChangeTracker.Clear();
                return OperationResult.Fail("لا يمكن حذف المستخدم لارتباطه ببيانات (مراكز، حلقات، طلاب...). يمكنك إيقاف الحساب بدلاً من الحذف.");
            }
            await _realtime.ForceLogoutAsync(id, "تم حذف حسابك");
            await _activity.LogAsync(ActivityActions.UserDeleted, id, user.FullName);
            await _realtime.UsersChangedAsync(id, "deleted");
            return OperationResult.Success("تم حذف المستخدم");
        }

        // ---------------------------------------------------------------- activity
        public async Task<PagedResult<ActivityDto>> GetActivityAsync(string? userId, int page, int pageSize)
        {
            page = Math.Max(1, page); pageSize = Math.Clamp(pageSize, 5, 100);
            var q = _db.UserActivities.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(userId)) q = q.Where(a => a.UserId == userId);
            var total = await q.CountAsync();
            var items = await (from a in q.OrderByDescending(a => a.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize)
                               join u in _db.Users on a.UserId equals u.Id into gu
                               from u in gu.DefaultIfEmpty()
                               select new ActivityDto
                               {
                                   Id = a.Id, UserId = a.UserId, UserName = u != null ? u.FullName : null, ActorName = a.ActorName,
                                   Action = a.Action, Details = a.Details, IpAddress = a.IpAddress, UserAgent = a.UserAgent, CreatedAt = a.CreatedAt
                               }).ToListAsync();
            items.ForEach(i => i.ActionAr = ActivityActions.ToArabic(i.Action));
            return new PagedResult<ActivityDto> { Items = items, Total = total, Page = page, PageSize = pageSize };
        }

        public static string TranslateIdentityError(IdentityError e) => e.Code switch
        {
            "PasswordTooShort" => "كلمة المرور قصيرة جدًا",
            "PasswordRequiresNonAlphanumeric" => "كلمة المرور يجب أن تحتوي على رمز مثل $ أو @",
            "PasswordRequiresDigit" => "كلمة المرور يجب أن تحتوي على رقم",
            "PasswordRequiresLower" => "كلمة المرور يجب أن تحتوي على حرف إنجليزي صغير",
            "PasswordRequiresUpper" => "كلمة المرور يجب أن تحتوي على حرف إنجليزي كبير",
            "DuplicateUserName" => "اسم المستخدم مستخدم من قبل",
            "DuplicateEmail" => "البريد الإلكتروني مستخدم من قبل",
            "InvalidUserName" => "اسم المستخدم يجب أن يحتوي على حروف إنجليزية وأرقام فقط",
            "InvalidEmail" => "البريد الإلكتروني غير صحيح",
            "DuplicateRoleName" => "اسم المجموعة موجود من قبل",
            _ => e.Description
        };
    }
}
