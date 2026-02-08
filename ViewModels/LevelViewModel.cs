namespace ZekrHakeem_v11.ViewModels
{
    public class LevelViewModel
    {
        public List<LevelData> Levels { get; set; }

        public List<LogLevelData> LogLevels { get; set; }

        public LevelData NewLevel { get; set; }

        public LessonViewModel? LessonsOfLevel { get; set; }
        public StudentViewModel? StudentsOfLevel { get; set; }
    }
}
