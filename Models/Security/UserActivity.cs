namespace ZekrHakeem_v11.Models.Security
{
    [Table("UserActivities")]
    [Index(nameof(UserId), nameof(CreatedAt))]
    public class UserActivity
    {
        public long Id { get; set; }

        /// <summary>المستخدم صاحب النشاط أو المستهدف به</summary>
        [MaxLength(450)]
        public string? UserId { get; set; }

        /// <summary>المستخدم الذي قام بالإجراء (المسؤول مثلاً)</summary>
        [MaxLength(450)]
        public string? ActorId { get; set; }

        [MaxLength(200)]
        public string? ActorName { get; set; }

        [Required, MaxLength(60)]
        public string Action { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Details { get; set; }

        [MaxLength(64)]
        public string? IpAddress { get; set; }

        [MaxLength(400)]
        public string? UserAgent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
