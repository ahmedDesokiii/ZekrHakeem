using ZekrHakeem_v11.Dtos;

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesEpisodeProg
    {
        List<EpisodeProg> GetEpisodeProgsForMen();
        List<EpisodeProg> GetEpisodeProgsForWomen();
        List<EpisodeProg> GetEpisodeProgsWithSupervisor(string userid);
        List<EpisodeProg> GetEpisodeProgsWithTeacher(string userid);
        List<EpisodeProg> GetEpisodeProgsWithProg(Guid prid);

        EpisodeProg? GetById(Guid id);
        bool UpdateLessonsOfStudents(List<StudentProg> Students);
        StudentProgViewModel GetStudentProgsForEpisodeProg(Guid epid);

        //bool UpdateLessonsOfStudents();
        void StartEpisodeProg(Guid id);
        void ManualEndEpisodeProg(Guid id);
        void AutoEndEpisodeProg(Guid id);
        public void ResetAllEpisodeProgs();

        Dictionary<StudentProg, EvaluationProg> GetStudentProgLastEvaluate(List<StudentProg> students);

        bool Pause(Guid Id);
        bool Resume(Guid Id);
        PrayerTimingsDto GetPrayerTimesAPI();
        //List<Surah> GetAllSurahsWithStudent(Guid id);
        //Attendance? GetTeacherAttendByEpisodeProgId(Guid epid, DateTime date);
        Attendance FindAttendanceByEpisodeProgId(Guid epid);
        // void ResetEpisodeProg(Guid id, EpisodeProgViewModel epvm);

        //bool UpdateLocationState(EpisodeProgViewModel model);

        bool AddEvaluatoinOnly(List<EvaluationProg> evaluations);

        ////////////////////
        /// Student Prog Services
        /// 
        StudentProg FindStudentProgBy(Guid Id);

        StudentProg FindStudentProgBy(string Name);

        bool SaveStudentProg(StudentProg model);
        bool AssignedStudentsToEpisodeProg(EpisodeProgViewModel model);


    }
}
