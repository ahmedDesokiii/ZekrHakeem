namespace ZekrHakeem_v11.ViewModels
{
    public class CenterViewModel
    {
        public List<Center>? Centers { get; set; }
        public List<LogCenter>? LogCenters { get; set; }
        public Center? NewCenter { get; set; }
        public List<Center>? CentersForMen { get; set; }
        public List<Center>? CentersForWomen { get; set; }
        public List<Center>? CentersForSupervisorCenter { get; set; }
        public LocationViewModel? LocationsOfCenter { get; set; }
        public EpisodeViewModel? EpisodesOfCenter { get; set; }
        public StudentViewModel? StudentsOfCenter { get; set; }

        public IEnumerable<SelectListItem>? SupervisorsMenCenter { get; set; }
        public IEnumerable<SelectListItem>? SupervisorsWomenCenter { get; set; }

        //public List<UserViewModel> SupervisorsMenCenter { get; set; }
        //public List<UserViewModel> SupervisorsWomenCenter { get; set; }



    }
}
