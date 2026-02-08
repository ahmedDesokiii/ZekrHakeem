namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesLevelData
    {
        LevelData? GetById(Guid id);
        LessonViewModel GetLessonsForLevelData(Guid LevelDataid);
        StudentViewModel GetStudentsForLevelData(Guid LevelDataid);
        LevelViewModel GetAllLevelsWithLessons();
    }
}
