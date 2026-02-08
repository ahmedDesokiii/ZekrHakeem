namespace ZekrHakeem_v11.ViewModels
{
    public class ProgViewModel
    {
        public List<Prog>? Progs { get; set; }
        public List<LogProg>? LogProgs { get; set; }
        public Prog? NewProg { get; set; }

        public List<Prog>? ProgsForMen { get; set; }
        public List<Prog>? ProgsForWomen { get; set; }
        public List<Prog>? ProgsForSupervisorProg { get; set; }
        public LocationViewModel? LocationsOfProg { get; set; }
        public EpisodeProgViewModel? EpVM { get; set; }
        public List<EpisodeProg>? EpisodeProgsOfProg { get; set; }
        public StudentViewModel? StudentsOfProg { get; set; }
        public List<UserViewModel> SupervisorsMenProg { get; set; }
        public IEnumerable<SelectListItem> SupervisorsMenProgList { get; set; }
        public IEnumerable<SelectListItem> SupervisorsWomenProgList { get; set; }
        public List<UserViewModel> SupervisorsWomenProg { get; set; }
        public List<PlanData>? PlansData { get; set; }


    }
}
