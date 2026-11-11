namespace ZekrHakeem_v11.Models.Security
{
    [Table("RolePermissions")]
    [PrimaryKey(nameof(RoleId), nameof(PermissionId))]
    public class RolePermission
    {
        [MaxLength(450)]
        public string RoleId { get; set; } = string.Empty;
        [ForeignKey(nameof(RoleId))]
        public IdentityRole? Role { get; set; }

        public int PermissionId { get; set; }
        [ForeignKey(nameof(PermissionId))]
        public AppPermission? Permission { get; set; }

        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(450)]
        public string? GrantedBy { get; set; }
    }
}
