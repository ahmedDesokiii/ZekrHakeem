namespace ZekrHakeem_v11.ViewModels
{
    public class StudentProgViewModel
    {
        public List<StudentProg>? StudentProgs { get; set; }
        public List<StudentProg> StudentProgsForMen { get; set; }
        public List<StudentProg> StudentProgsForWomen { get; set; }
        public List<StudentProg>? StudentProgsWithSupervisor { get; set; }
        public List<StudentProg>? StudentProgsWithTeacher { get; set; }
        public List<StudentProg>? StudentProgsWithParent { get; set; }
        public StudentProg? NewStudentProg { get; set; }

        public EpisodeProgViewModel EVM { get; set; }
        public EpisodeProg SelectedEpisodeProg { get; set; }

        public List<Evaluation>? Evaluations { get; set; }
        public LevelViewModel? LevelsData { get; set; }
        public List<PlanData>? PlansData { get; set; }


        public List<LessonData>? LessonsData { get; set; }
        public List<Surah>? Surahs { get; set; }

        public IEnumerable<SelectListItem>? Surahs_ctp { get; set; }
        public IEnumerable<SelectListItem>? Surahs_ptc { get; set; }

        public IEnumerable<Verse>? Verses { get; set; }
        public Verse? Verse { get; set; }

        public List<LogEpisodeProg>? LogEpisodeProgs { get; set; }
        public List<EpisodeProg>? EpisodeProgs { get; set; }
        public List<EpisodeProg>? EpisodeProgsForMen { get; set; }
        public List<EpisodeProg>? EpisodeProgsForWomen { get; set; }
        public List<EpisodeProg>? EpisodeProgsWithSupervisor { get; set; }
        public List<EpisodeProg>? EpisodeProgsWithTeacher { get; set; }
        public IEnumerable<SelectListItem>? AvilableNationalities { get; set; }

    }
}
