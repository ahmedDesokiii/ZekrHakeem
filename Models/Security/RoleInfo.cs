namespace ZekrHakeem_v11.Models.Security
{
    /// <summary>
    /// بيانات إضافية للمجموعة (Role) بدون تغيير جدول AspNetRoles.
    /// </summary>
    [Table("RoleInfos")]
    public class RoleInfo
    {
        [Key, MaxLength(450)]
        public string RoleId { get; set; } = string.Empty;
        [ForeignKey(nameof(RoleId))]
        public IdentityRole? Role { get; set; }

        [MaxLength(150)]
        public string? NameAr { get; set; }

        [MaxLength(400)]
        public string? Description { get; set; }

        [MaxLength(20)]
        public string Color { get; set; } = "#15B392";

        [MaxLength(60)]
        public string Icon { get; set; } = "bi-people-fill";

        /// <summary>مجموعة نظام لا يمكن حذفها أو تغيير اسمها</summary>
        public bool IsSystem { get; set; }

        /// <summary>المجموعة الافتراضية للمستخدمين الجدد عند التسجيل</summary>
        public bool IsDefaultForRegistration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
