using Newtonsoft.Json;

namespace ZekrHakeem_v11.Models
{
    [Index(nameof(IdentityNumber), IsUnique = true)]
    public class Student
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "فضلا , ادخل اسم الطالب الرباعي بشكل صحيح")]
        [MinLength(6, ErrorMessage = ("اسم الطالب لا يقل عن 6 خانات "))]
        [MaxLength(50, ErrorMessage = "اسم الطالب لا يزيد عن 50 خانة")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "فضلا , ادخل هوية الطالب بشكل صحيح")]
        [MinLength(10, ErrorMessage = "هوية الطالب لا تقل عن 10 خانات")]
        [MaxLength(30, ErrorMessage = "هوية الطالب لا تزيد عن 30 خانة")] //Unique
        public string? IdentityNumber { get; set; }

        [Required(ErrorMessage = "فضلا , حدد نوع هوية الطالب")]
        [MaxLength(20)]
        public string? Category { get; set; } // مواطن | زائر

        //[Required(ErrorMessage = "فضلا , حدد نوع هوية الطالب")]
        //[MaxLength(20)]
        public string? Gender { get; set; }

        [MaxLength(100)]
        public string? Adress { get; set; }
        public string? ProfilePicture { get; set; }
        [MaxLength(1000)]
        public string? Notes { get; set; }

        //[Required(ErrorMessage = "فضلا , حدد ميلاد الطالب")]
        public string? BirthDate { get; set; }

        [Required(ErrorMessage = "فضلا , حدد الحلقة المتاحة")]
        public Guid StEpisodeId { get; set; }
        [ForeignKey("StEpisodeId")]
        public Episode StEpisode { get; set; }


        public bool? newLessonState { get; set; } = false;
        public bool? inProg { get; set; } = false;
        public bool? IsEvaluate { get; set; } = false;

        //[Required(ErrorMessage = "فضلا , حدد الدرس للطالب")]
        public int? LessonDataId { get; set; }
        [ForeignKey("LessonDataId")]
        public LessonData? LessonData { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required(ErrorMessage = "فضلا , حدد جنسية الطالب")]
        public Guid NationalityId { get; set; }
        [ForeignKey("NationalityId")]
        public Nationality Nationality { get; set; }

        [Required(ErrorMessage = "فضلا , حدد الحالة الصحية للطالب")]
        public Guid HealthConditionId { get; set; }
        [ForeignKey("HealthConditionId")]
        public HealthCondition? HealthCondition { get; set; }

        [Required(ErrorMessage = "فضلا , حدد نوع التعليم للطالب")]
        [MaxLength(20)]
        public string? EducationType { get; set; } //تحفيظ | عام 

        [Required(ErrorMessage = "فضلا , ادخل هوية ولي الأمر ")]
        [MinLength(10, ErrorMessage = "هوية ولي الأمر لا تقل عن 10 خانات")]
        [MaxLength(30, ErrorMessage = "هوية ولي الأمر لا تقل عن 30 خانة")]
        public string? ParentIdentityNumber { get; set; }

        [Required(ErrorMessage = "فضلا , ادخل جوال ولي الأمر .. يفضل رقم واتس آب")]
        [Phone]
        [MinLength(14, ErrorMessage = "جوال ولي الأمر لا تقل عن 14 خانة , ويبدأ بمفتاح الدولة : 00966500000000")]
        [MaxLength(30, ErrorMessage = "جوال ولي الأمر لا تزيد عن 30 خانة")]
        public string? ParentPhoneNumber { get; set; }

        public string? SaveDirection { get; set; }

        [Required(ErrorMessage = "فضلا , حدد الخطة ")]
        public Guid? PlanDataId { get; set; } = null;
        [ForeignKey("PlanDataId")]
        public PlanData? PlanData { get; set; }


        public int? VerseId { get; set; } = null;
        [ForeignKey("VerseId")]
        public Verse? Verse { get; set; }

        public int? SurahId { get; set; } = null;
        [ForeignKey("SurahId")]
        public Surah? Surah { get; set; }

        public int? PrevRevVerseFrom { get; set; }
        public string? PrevRevSurhFrom { get; set; }
        public int? PrevRevVerseTo { get; set; }
        public string? PrevRevSurahTo { get; set; }
        public int? PrevSaveVerseTo { get; set; }
        public string? PrevSaveSurhTo { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
        public ICollection<Demand> Demands { get; set; } = new List<Demand>();

        public string? EpProgName { get; set; } = "";

        public int? CurrentState { get; set; }

    }
}
