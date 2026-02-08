namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesReport
    {
        public List<Attendance> GetAllAttendance();
        public List<Attendance> GetAllAttendanceMen();
        public List<Attendance> GetAllAttendanceWomen();
        public List<Student> GetAllStudents();
        public List<Student> GetAllStudentsMen();
        public List<Student> GetAllStudentsWomen();
        public List<Student> GetAllStudentsWithTeacher(string teacher);

        public List<Attendance> GetTeacherAttendance(string teacherId);
        public ApplicationUser? GetById(string id);
        public void CheckEmptyEvaluations(List<Student> Students);
        public Dictionary<Student, Evaluation> GetStudentFristEvaluate(List<Student> students, List<Evaluation> Evals);
        public Dictionary<Student, Evaluation> GetStudentLastEvaluate(List<Student> students, List<Evaluation> Evals);



        public Dictionary<Student, float?> GetTotalPages_Save(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv);
        public Dictionary<Student, float?> GetTotalPages_Rev(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv);
        public Dictionary<Student, float?> GetTotalPages_Rabt(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv);

        public Dictionary<Student, float?> GetTotalSections_Save(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv);
        public Dictionary<Student, float?> GetTotalSections_Rev(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv);
        public Dictionary<Student, float?> GetTotalSections_Rabt(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv);

        public Dictionary<Student, int> GetCountAttend_0(List<Student> students, List<Evaluation> Evals);
        public Dictionary<Student, int> GetCountAttend_1(List<Student> students, List<Evaluation> Evals);
        public Dictionary<Student, int> GetCountAttend_2(List<Student> students, List<Evaluation> Evals);
        public Dictionary<Student, int> GetCountAttend_3(List<Student> students, List<Evaluation> Evals);
        public Dictionary<Student, int> GetCountAttend_4(List<Student> students, List<Evaluation> Evals);
        public Dictionary<Student, int> GetCountAttend_5(List<Student> students, List<Evaluation> Evals);

    }
}
