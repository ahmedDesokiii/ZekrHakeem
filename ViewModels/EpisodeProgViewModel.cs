using ZekrHakeem_v11.Dtos;

namespace ZekrHakeem_v11.ViewModels
{
    public class EpisodeProgViewModel
    {
        public EpisodeProg? NewEpisodeProg { get; set; }
        public EpisodeProg? EpisodeProgInTime { get; set; }
        public List<LogEpisodeProg>? LogEpisodeProgs { get; set; }
        public List<EpisodeProg>? EpisodeProgs { get; set; }
        public List<EpisodeProg>? EpisodeProgsForMen { get; set; }
        public List<EpisodeProg>? EpisodeProgsForWomen { get; set; }
        public List<EpisodeProg>? EpisodeProgsWithSupervisor { get; set; }
        public List<EpisodeProg>? EpisodeProgsWithTeacher { get; set; }
        public Dictionary<StudentProg, EvaluationProg>? LastEvaluateOfStudentProgs { get; set; }

        public LocationViewModel? LVM { get; set; }
        public StudentProgViewModel StudentProgsVM { get; set; }
        public StudentViewModel StudentsVM { get; set; }
        public List<EvaluationProg>? EvaluationProgs { get; set; }
        public List<UserViewModel>? TeachersMen { get; set; }
        public List<UserViewModel>? TeachersWomen { get; set; }

        public PrayerTimingsDto? PrayerTimesAPI { get; set; }

        public IEnumerable<SelectListItem>? AvilableNationalities { get; set; }
        public Attendance? TeacherAttendance { get; set; }
        public IEnumerable<SelectListItem>? Surahs_ctp { get; set; }
        public IEnumerable<SelectListItem>? Surahs_ptc { get; set; }
        public IEnumerable<SelectListItem>? Progs { get; set; }

        public IEnumerable<SelectListItem>? EpisodeProgsList { get; set; }
        public IEnumerable<SelectListItem>? EpisodeProgsForMenList { get; set; }
        public IEnumerable<SelectListItem>? EpisodeProgsForWomenList { get; set; }
        public IEnumerable<SelectListItem>? EpisodeProgsWithSupervisorList { get; set; }
        public IEnumerable<SelectListItem>? EpisodeProgsWithTeacherList { get; set; }

        public List<Student> Students { get; set; }
        public List<Student> StudentsForMen { get; set; }
        public List<Student> StudentsForWomen { get; set; }
        public IEnumerable<Verse>? Verses { get; set; }
        public StudentProg NewStudentProg { get; set; }

        public Guid SelectedEpProgId { get; set; }

    }
}
