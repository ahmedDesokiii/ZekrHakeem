namespace ZekrHakeem_v11.Models.Security
{
    /// <summary>
    /// صلاحية مباشرة على مستخدم بعينه (استثناء على صلاحيات مجموعاته).
    /// IsGranted = true  => منح إضافي
    /// IsGranted = false => منع حتى لو كانت ممنوحة عن طريق مجموعة
    /// </summary>
    [Table("UserPermissions")]
    [PrimaryKey(nameof(UserId), nameof(PermissionId))]
    public class UserPermission
    {
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public int PermissionId { get; set; }
        [ForeignKey(nameof(PermissionId))]
        public AppPermission? Permission { get; set; }

        public bool IsGranted { get; set; } = true;

        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(450)]
        public string? GrantedBy { get; set; }
    }
}
