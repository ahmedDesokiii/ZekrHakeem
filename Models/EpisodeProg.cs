namespace ZekrHakeem_v11.Models
{
    public class EpisodeProg
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Name { get; set; }

        public string? AttendType { get; set; }
        //public int LimitStudents { get; set; } = 0;
        public bool Status { get; set; } = false;
        public bool ManualFinished { get; set; } = false;
        public bool IsAvilable { get; set; } = false;

        public string? Description { get; set; }
        public DateTime? CheckDate { get; set; }

        public string? Period { get; set; }

        public Guid? EpProgLocationId { get; set; }
        [ForeignKey("EpProgLocationId")]
        public Location? EpProgLocation { get; set; }

        public Guid? EpProgId { get; set; }
        [ForeignKey("EpProgId")]
        public Prog? EpProg { get; set; }

        public ICollection<StudentProg> StudentProgs { get; set; }
        //public List<StudentProgEpisodeProg> StudentProgEpisodeProgs { get; set; } = new List<StudentProgEpisodeProg>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public int? StartSurah { get; set; }
        public int? EndSurah { get; set; }
        public string? SaveDir { get; set; }

        public int? PauseState { get; set; } = 1;
        public int? CurrentState { get; set; }
    }
}
