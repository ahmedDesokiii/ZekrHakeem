namespace ZekrHakeem_v11.ViewModels
{
    public class StudentViewModel
    {
        public List<Student>? Students { get; set; }
        public List<Student>? StudentsForMen { get; set; }
        public List<Student>? StudentsForWomen { get; set; }
        public List<Student>? StudentsWithSupervisor { get; set; }
        public List<Student>? StudentsWithTeacher { get; set; }
        public List<Student>? StudentsWithParent { get; set; }
        public Student? NewStudent { get; set; }
        public Demand? NewDemand { get; set; }
        public List<LogStudent>? LogStudents { get; set; }

        public List<Center>? Centers { get; set; }
        public EpisodeViewModel? EVM { get; set; }

        public List<Demand>? Demands { get; set; }
        public List<Evaluation>? Evaluations { get; set; }
        public LevelViewModel? LevelsData { get; set; }
        public IEnumerable<SelectListItem>? ListLevels { get; set; }

        public List<PlanData>? PlansData { get; set; }

        public List<Nationality>? Nationalities { get; set; }
        public Nationality? NewNationality { get; set; }
        public List<HealthCondition>? HealthConditions { get; set; }
        public HealthCondition? NewHealth { get; set; }

        public List<LessonData>? LessonsData { get; set; }
        public List<Surah>? Surahs { get; set; }

        public IFormFile? StProfileImage { get; set; }

        //
        public IEnumerable<SelectListItem>? Surahs_ctp { get; set; }
        public IEnumerable<SelectListItem>? Surahs_ptc { get; set; }
        public IEnumerable<SelectListItem>? AvilableNationalities { get; set; }
        public IEnumerable<SelectListItem>? AvilableHealthConditions { get; set; }
        public IEnumerable<Verse>? Verses { get; set; }
        public Verse? Verse { get; set; }

        public Episode? SelectedEpisode { get; set; }
        public List<LogEpisode>? LogEpisodes { get; set; }
        public List<Episode>? Episodes { get; set; }
        public List<Episode>? EpisodesForMen { get; set; }
        public List<Episode>? EpisodesForWomen { get; set; }
        public List<Episode>? EpisodesWithSupervisor { get; set; }
        public List<Episode>? EpisodesWithTeacher { get; set; }

        public IEnumerable<SelectListItem>? EPisodes_List { get; set; }
        public IEnumerable<SelectListItem>? EPisodesMen_List { get; set; }
        public IEnumerable<SelectListItem>? EPisodesWomen_List { get; set; }
        public IEnumerable<SelectListItem>? EPisodesSupervisor_List { get; set; }


    }
}
