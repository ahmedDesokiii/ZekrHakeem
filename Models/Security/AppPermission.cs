namespace ZekrHakeem_v11.Models.Security
{
    /// <summary>
    /// صلاحية واحدة مثل Permissions.Centers.View
    /// القيمة Key هي نفس اسم الـ Policy المستخدم في [Authorize(Policy = ...)]
    /// </summary>
    [Table("AppPermissions")]
    [Index(nameof(Key), IsUnique = true)]
    public class AppPermission
    {
        public int Id { get; set; }

        public int ModuleId { get; set; }
        [ForeignKey(nameof(ModuleId))]
        public AppModule? Module { get; set; }

        [Required, MaxLength(200)]
        public string Key { get; set; } = string.Empty;

        [Required, MaxLength(60)]
        public string Action { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
