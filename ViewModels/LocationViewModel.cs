namespace ZekrHakeem_v11.ViewModels
{
    public class LocationViewModel
    {
        public List<Location> Locations { get; set; }
        public List<Center> Centers { get; set; }
        public List<LogLocation> LogLocations { get; set; }
        public Location NewLocation { get; set; }

        public CenterViewModel CVM { get; set; }

        public List<Location> LocationsForMen { get; set; }
        public List<Center> CentersForMen { get; set; }
        public List<Location> LocationsForWomen { get; set; }
        public List<Center> CentersForWomen { get; set; }

        public List<Center>? CentersForSupervisorCenter { get; set; }
        public List<District>? Districts { get; set; }
        public List<Location> LocationsWithSupervisor { get; set; }
        public EpisodeViewModel? EpisodesOfLocation { get; set; }
        public StudentViewModel? StudentsOfLocation { get; set; }

        //public DistrictViewModel? DistrictsOfCenter { get; set; }


    }
}
