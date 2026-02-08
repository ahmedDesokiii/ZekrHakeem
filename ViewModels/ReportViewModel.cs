namespace ZekrHakeem_v11.ViewModels
{
    public class ReportViewModel
    {
        //Teacher attendance Reports
        public IEnumerable<Attendance> AllAttendances { get; set; }
        public IEnumerable<Attendance> AllAttendancesMen { get; set; }
        public IEnumerable<Attendance> AllAttendancesWomen { get; set; }
        public IEnumerable<Attendance> TeacherAttendances { get; set; }
        public ApplicationUser Teacher { get; set; }

        public IEnumerable<SelectListItem>? AllEpisodes { get; set; }
        public IEnumerable<SelectListItem>? EpisodesMen { get; set; }
        public IEnumerable<SelectListItem>? EpisodesTeacher { get; set; }
        public IEnumerable<SelectListItem>? EpisodesWomen { get; set; }
        public IEnumerable<SelectListItem>? TeachersMen { get; set; }
        public IEnumerable<SelectListItem>? TeachersWomen { get; set; }

        // Students Evaluations Reports
        public List<Student> AllStudentsMen { get; set; }
        public List<Student> AllStudentsWomen { get; set; }
        public List<Student> AllStudents { get; set; }
        public List<Student> AllStudentsWithTeacher { get; set; }

        public Dictionary<Student, LessonData>? NextLessonData_AllStudent { get; set; }
        public Dictionary<Student, LessonData>? NextLessonData_AllStudentMen { get; set; }
        public Dictionary<Student, LessonData>? NextLessonData_AllStudentWomen { get; set; }
        public Dictionary<Student, LessonData>? NextLessonData_AllStudentWithTeacher { get; set; }

        ///////////// /////////////

        public IEnumerable<SelectListItem>? StudentsAll { get; set; }
        public IEnumerable<SelectListItem>? StudentsMen { get; set; }
        public IEnumerable<SelectListItem>? StudentsWomen { get; set; }
        public IEnumerable<SelectListItem>? Plans { get; set; }
        public IEnumerable<SelectListItem>? Nationalities { get; set; }
        public List<Evaluation> Evaluations { get; set; }
        public Dictionary<Student, Evaluation>? FristEvaluateOfStudents { get; set; }
        public Dictionary<Student, Evaluation>? LastEvaluateOfStudents { get; set; }
        public Dictionary<Student, float?>? SavePages { get; set; }
        public Dictionary<Student, float?>? RevPages { get; set; }
        public Dictionary<Student, float?>? RabtPages { get; set; }

        public Dictionary<Student, float?>? SaveSections { get; set; }
        public Dictionary<Student, float?>? RevSections { get; set; }
        public Dictionary<Student, float?>? RabtSections { get; set; }

        public Dictionary<Student, int>? CountAttend_0 { get; set; }
        public Dictionary<Student, int>? CountAttend_1 { get; set; }
        public Dictionary<Student, int>? CountAttend_2 { get; set; }
        public Dictionary<Student, int>? CountAttend_3 { get; set; }
        public Dictionary<Student, int>? CountAttend_4 { get; set; }
        public Dictionary<Student, int>? CountAttend_5 { get; set; }

        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
