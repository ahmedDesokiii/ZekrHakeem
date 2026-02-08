namespace ZekrHakeem_v11.Models
{
    public class Demand
    {
        public Guid Id { get; set; }
        [MaxLength(80)]
        public string? Request_Type { get; set; }
        public DateTime? Request_Date { get; set; }
        public string? Request_Reason { get; set; }
        [MaxLength(80)]
        public string? OldEpisode { get; set; }
        [MaxLength(80)]
        public string? NewEpisode { get; set; }
        [MaxLength(80)]
        public string? NotificationText { get; set; }
        public string? StName { get; set; }
        public string? StIdentityNum { get; set; }
        public string? StParPhoneNum { get; set; }
        public string? StAdress { get; set; }
        public string? StNotes { get; set; }
        public bool? IsOpened { get; set; } = false;
        public bool? IsAccepted { get; set; } = false;

        public DateTime? StartHoliday { get; set; }
        public DateTime? EndHoliday { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public Guid? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public int? CurrentState { get; set; }
    }
}
