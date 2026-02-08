namespace ZekrHakeem_v11.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }

        public string? Status_AttIn { get; set; }
        public string? Status_AttOut { get; set; }

        public DateTime Att_In { get; set; }
        public DateTime Att_Out { get; set; }

        public string? ApplicationUserId { get; set; } = null;
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public Guid? EpisodeId { get; set; } = null;
        [ForeignKey("EpisodeId")]
        public Episode? Episode { get; set; }
        public Guid? EpisodeProgId { get; set; } = null;
        [ForeignKey("EpisodeProgId")]
        public EpisodeProg? EpisodeProg { get; set; }

        public int? CurrentState { get; set; }

    }
}
