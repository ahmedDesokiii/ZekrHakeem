namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesLocation
    {
        List<Location> GetLocationsForMen();
        List<Location> GetLocationsForWomen();
        List<Location> GetLocationsWithSupervisor(string userid);
        Location? GetById(Guid id);
        bool Pause(Guid Id);
        bool Resume(Guid Id);
        EpisodeViewModel GetEpisodesForLocation(Guid locationid);
        StudentViewModel GetStudentsForLocation(Guid locationid);
    }
}
