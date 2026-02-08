using ZekrHakeem_v11.Dtos;

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesEpisode
    {
        List<Episode> GetEpisodesForMen();
        List<Episode> GetEpisodesForWomen();
        List<Episode> GetEpisodesWithSupervisor(string userid);
        List<Episode> GetEpisodesWithTeacher(string userid);

        Episode? GetById(Guid id);
        bool UpdateLessonsOfStudents(List<Student> Students);
        StudentViewModel GetStudentsForEpisode(Guid epid);
        void StartEpisode(Guid id);
        void ManualEndEpisode(Guid id);
        void AutoEndEpisode(Guid id);
        public void ResetAllEpisodes();


        Dictionary<Student, LessonData> GetStudentPrevLesson(List<Student> students);
        Dictionary<Student, LessonData> GetStudentNextLesson(List<Student> students);
        Dictionary<Student, Evaluation> GetStudentLastEvaluate(List<Student> students);

        List<EpisodeType> GetEpisodeTypes();
        EpisodeType FindByEpTypeId(Guid Id);
        EpisodeType FindByEpTypeName(string Name);
        bool SaveEpisodeType(EpisodeType model);
        bool DeleteEpisodeType(Guid Id);
        bool Pause(Guid Id);
        bool Resume(Guid Id);
        PrayerTimingsDto GetPrayerTimesAPI();
        //List<Surah> GetAllSurahsWithStudent(Guid id);
        //Attendance? GetTeacherAttendByEpisodeId(Guid epid, DateTime date);
        Attendance FindAttendanceByEpisodeId(Guid epid);
        // void ResetEpisode(Guid id, EpisodeViewModel epvm);

        //bool UpdateLocationState(EpisodeViewModel model);

        bool AddEvaluatoinOnly(List<Evaluation> evaluations);
    }
}
