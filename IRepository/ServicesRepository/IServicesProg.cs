namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesProg
    {
        List<Prog> GetProgsForMen();
        List<Prog> GetProgsForWomen();
        List<Prog> GetProgsWithSupervisor(string userid);

        Prog? GetById(Guid id);
        //LocationViewModel GetLocationsForProg(Guid centerid);
        //EpisodeViewModel GetEpisodesForProg(Guid centerid);
        //StudentViewModel GetStudentsForProg(Guid centerid);

        bool Pause(Guid Id);
        bool Resume(Guid Id);
    }
}
