namespace ZekrHakeem_v11.Models
{
    public class Evaluation
    {
        public Guid Id { get; set; }
        public DateTime Ev_Date { get; set; }
        public int Ev_Save { get; set; }
        public int Ev_Rabt { get; set; }
        public int Ev_Rev { get; set; }
        public int Ev_Attend { get; set; }
        public string? Notes { get; set; }

        public int? Ev_RevSurahIdFrom { get; set; } = null;
        [MaxLength(20)]
        public string? Ev_RevSurahNameFrom { get; set; } = null;
        public int? Ev_RevAyahIdFrom { get; set; } = null;
        public int? Ev_RevSurahIdTo { get; set; } = null;
        [MaxLength(20)]
        public string? Ev_RevSurahNameTo { get; set; } = null;
        public int? Ev_RevAyahIdTo { get; set; } = null;

        public int? Ev_RabtSurahIdFrom { get; set; } = null;
        [MaxLength(20)]
        public string? Ev_RabtSurahNameFrom { get; set; } = null;
        public int? Ev_RabtAyahIdFrom { get; set; } = null;
        public int? Ev_RabtSurahIdTo { get; set; } = null;
        [MaxLength(20)]
        public string? Ev_RabtSurahNameTo { get; set; } = null;
        public int? Ev_RabtAyahIdTo { get; set; } = null;

        public int? Ev_SaveSurahIdFrom { get; set; } = null;
        [MaxLength(20)]
        public string? Ev_SaveSurahNameFrom { get; set; } = null;
        public int? Ev_SaveAyahIdFrom { get; set; } = null;
        public int? Ev_SaveSurahIdTo { get; set; } = null;
        [MaxLength(20)]
        public string? Ev_SaveSurahNameTo { get; set; } = null;
        public int? Ev_SaveAyahIdTo { get; set; } = null;

        public Guid? EvStudentId { get; set; }
        [ForeignKey("EvStudentId")]
        public Student? EvStudent { get; set; }

        public int? CurrentState { get; set; }

    }
}
