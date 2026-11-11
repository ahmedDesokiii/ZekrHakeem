namespace ZekrHakeem_v11.Dtos.Security
{
    public class OperationResult
    {
        public bool Ok { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public object? Data { get; set; }

        public static OperationResult Success(string? message = null, object? data = null) => new() { Ok = true, Message = message, Data = data };
        public static OperationResult Fail(params string[] errors) => new() { Ok = false, Errors = errors.ToList(), Message = errors.FirstOrDefault() };
        public static OperationResult Fail(IEnumerable<string> errors) => Fail(errors.ToArray());
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Pages => PageSize == 0 ? 0 : (int)Math.Ceiling(Total / (double)PageSize);
    }

    public class UserQuery
    {
        public string? Search { get; set; }
        public string? RoleId { get; set; }
        /// <summary>all | active | inactive | online | unconfirmed</summary>
        public string? Status { get; set; }
        public string? Gender { get; set; }
        /// <summary>name | username | created | lastLogin</summary>
        public string? Sort { get; set; }
        public bool Desc { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }

    public class RoleChipDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public string Color { get; set; } = "#15B392";
        public string Icon { get; set; } = "bi-people-fill";
    }

    public class UserListItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string IdentityNum { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsOnline { get; set; }
        public bool HasPicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public List<RoleChipDto> Roles { get; set; } = new();
    }

    public class UserDetailsDto : UserListItemDto
    {
        public string? Nationality { get; set; }
        public string? Adress { get; set; }
        public string? Notes { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int DirectPermissionsCount { get; set; }
        public int EffectivePermissionsCount { get; set; }
        public List<ActivityDto> RecentActivity { get; set; } = new();
    }

    public class UserEditDto
    {
        public string? Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string IdentityNum { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? Adress { get; set; }
        public string? Notes { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true;
        public List<string> RoleIds { get; set; } = new();
    }

    public class UsersStatsDto
    {
        public int Total { get; set; }
        public int Active { get; set; }
        public int Inactive { get; set; }
        public int Online { get; set; }
        public int Unconfirmed { get; set; }
        public int NewThisMonth { get; set; }
        public List<RoleCountDto> ByRole { get; set; } = new();
    }

    public class RoleCountDto
    {
        public string Id { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class ActivityDto
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ActorName { get; set; }
        public string Action { get; set; } = string.Empty;
        public string ActionAr { get; set; } = string.Empty;
        public string? Details { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RoleDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string Display => string.IsNullOrWhiteSpace(NameAr) ? Name : NameAr!;
        public string? Description { get; set; }
        public string Color { get; set; } = "#15B392";
        public string Icon { get; set; } = "bi-people-fill";
        public bool IsSystem { get; set; }
        public bool IsDefaultForRegistration { get; set; }
        public int UsersCount { get; set; }
        public int PermissionsCount { get; set; }
        public int TotalPermissions { get; set; }
        public List<string> SampleUsers { get; set; } = new();
    }

    public class RoleEditDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public string? Icon { get; set; }
        public bool IsDefaultForRegistration { get; set; }
        /// <summary>نسخ صلاحيات من مجموعة أخرى عند الإنشاء</summary>
        public string? CopyFromRoleId { get; set; }
    }

    public class PermissionMatrixDto
    {
        public string OwnerId { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public bool IsSuperAdmin { get; set; }
        public List<ModuleMatrixDto> Modules { get; set; } = new();
    }

    public class ModuleMatrixDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public List<PermissionCellDto> Permissions { get; set; } = new();
    }

    public class PermissionCellDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        /// <summary>للمجموعة: ممنوحة؟ للمستخدم: ممنوحة من خلال مجموعاته؟</summary>
        public bool Granted { get; set; }
        /// <summary>للمستخدم فقط: null بدون استثناء، true منح مباشر، false منع مباشر</summary>
        public bool? Override { get; set; }
        /// <summary>أسماء المجموعات التي تمنح هذه الصلاحية (للمستخدم)</summary>
        public List<string> Sources { get; set; } = new();
    }

    public class UserPermissionOverrideDto
    {
        public int PermissionId { get; set; }
        /// <summary>null = حذف الاستثناء</summary>
        public bool? State { get; set; }
    }
}
