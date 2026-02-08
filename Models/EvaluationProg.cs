namespace ZekrHakeem_v11.Models
{
    public class EvaluationProg
    {
        public Guid Id { get; set; }
        public DateTime Ev_Date { get; set; }
        public int Ev_Save { get; set; }
        public int Ev_Rabt { get; set; }
        public int Ev_Rev { get; set; }
        public int Ev_Attend { get; set; }
        public string? Notes { get; set; }

        public int? Ev_RevSurahIdFrom { get; set; } = null;
        public int? Ev_RevAyahIdFrom { get; set; } = null;

        public int? Ev_RevSurahIdTo { get; set; } = null;
        public int? Ev_RevAyahIdTo { get; set; } = null;

        public int? Ev_SaveSurahIdTo { get; set; } = null;
        public int? Ev_SaveAyahIdTo { get; set; } = null;

        public Guid? EvStudentProgId { get; set; }
        [ForeignKey("EvStudentProgId")]
        public StudentProg? EvStudentProg { get; set; }

        public int? CurrentState { get; set; }

    }
}
