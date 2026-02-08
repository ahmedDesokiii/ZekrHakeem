namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesCenter
    {
        List<Center> GetCentersForMen();
        List<Center> GetCentersForWomen();
        List<Center> GetCentersWithSupervisor(string userid);

        Center? GetById(Guid id);
        LocationViewModel GetLocationsForCenter(Guid centerid);
        EpisodeViewModel GetEpisodesForCenter(Guid centerid);
        StudentViewModel GetStudentsForCenter(Guid centerid);
        //StudentViewModel GetStudentsForLocations();
        bool Pause(Guid Id);
        bool Resume(Guid Id);
    }
}
