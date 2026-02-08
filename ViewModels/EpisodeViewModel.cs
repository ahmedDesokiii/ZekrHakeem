using ZekrHakeem_v11.Dtos;

namespace ZekrHakeem_v11.ViewModels
{
    public class EpisodeViewModel
    {
        //public Guid SelectEpId { get; set; }
        public Episode? NewEpisode { get; set; }
        public Episode? EpisodeInTime { get; set; }
        public List<LogEpisode>? LogEpisodes { get; set; }
        public List<Episode>? Episodes { get; set; }
        public List<Episode>? EpisodesForMen { get; set; }
        public List<Episode>? EpisodesForWomen { get; set; }
        public List<Episode>? EpisodesWithSupervisor { get; set; }
        public List<Episode>? EpisodesWithTeacher { get; set; }

        public Dictionary<Student, LessonData>? PrevLessonData { get; set; }
        public Dictionary<Student, LessonData>? NextLessonData { get; set; }
        public Dictionary<Student, Evaluation>? LastEvaluateOfStudents { get; set; }

        public LocationViewModel? LVM { get; set; }
        public StudentViewModel StudentsVM { get; set; }
        public List<Evaluation>? Evaluations { get; set; }
        public List<UserViewModel>? TeachersMen { get; set; }
        public List<UserViewModel>? TeachersWomen { get; set; }

        public List<EpisodeType>? EpisodeTypes { get; set; }
        public EpisodeType? NewEpisodeType { get; set; }
        public PrayerTimingsDto? PrayerTimesAPI { get; set; }
        public IFormFile? EpTypeImage { get; set; }


        public Attendance? TeacherAttendance { get; set; }
        public IEnumerable<SelectListItem>? Surahs_ctp { get; set; }
        public IEnumerable<SelectListItem>? Surahs_ptc { get; set; }
        public IEnumerable<SelectListItem>? AvilableEpisodeTypes { get; set; }
        public IEnumerable<Verse>? Verses { get; set; }

        //public List<Surah>? Surahs { get; set; }
        //public Verse? Verse { get; set; }
        public Student SelectStudent { get; set; }


        //public int SurahId { get; set; }
        //public IEnumerable<Surah>? ValidSurahs_ctp { get; set; }
        //public IEnumerable<Surah>? ValidSurahs_ptc { get; set; }
        //public int AyahId { get; set; }
        //public IEnumerable<Verse>? Ayahs { get; set; } = new List<Verse>();


    }
}
