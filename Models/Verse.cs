namespace ZekrHakeem_v11.Models
{
    public class Verse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int? Num { get; set; }

        [Required]
        public string? VerseText { get; set; } = string.Empty;

        public int? Letters { get; set; }
        public float? Lines { get; set; }

        public float? Sections_ctp { get; set; }
        public float? Sections_ptc { get; set; }
        public float? Pages_ctp { get; set; }
        public float? Pages_ptc { get; set; }

        public int? SurahId { get; set; } = null;
        [ForeignKey("SurahId")]
        public Surah? Surah { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        public int? CurrentState { get; set; }

    }
}
