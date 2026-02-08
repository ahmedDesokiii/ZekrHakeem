using Newtonsoft.Json;

namespace ZekrHakeem_v11.Models
{
    //[Index(nameof(IdentityNumber), IsUnique = true)]
    public class StudentProg
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [Phone]
        [MaxLength(30)]
        public string? ParentPhoneNumber { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public bool? outZH { get; set; } = false;

        public Guid? StEpisodeProgId { get; set; }
        [ForeignKey("StEpisodeProgId")]
        public EpisodeProg? EpisodeProg { get; set; }
        //public ICollection<EpisodeProg> StudentProgs { get; set; }

        //public List<StudentProgEpisodeProg> StudentProgEpisodeProgs { get; set; } = new List<StudentProgEpisodeProg>();
        [Required]
        public Guid NationalityId { get; set; }
        [ForeignKey("NationalityId")]
        public Nationality Nationality { get; set; }

        public ICollection<EvaluationProg> EvaluationProgs { get; set; } = new List<EvaluationProg>();
        public int? CurrentState { get; set; }

        ///////
        ///

        public bool? newLessonState { get; set; } = false;
        public bool? IsEvaluate { get; set; } = false;

        public int? LessonDataId { get; set; }
        [ForeignKey("LessonDataId")]
        public LessonData? LessonData { get; set; }

        public int? SurahId { get; set; } = null;
        [ForeignKey("SurahId")]
        public Surah? Surah { get; set; }

        public int? PrevSaveVerseTo { get; set; }
        public string? PrevSaveSurhTo { get; set; }
    }
}
