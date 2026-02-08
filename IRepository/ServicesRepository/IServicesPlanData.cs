namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesPlanData
    {
        PlanData? GetById(Guid id);
        LevelViewModel GetLevelsForPlan(Guid planid);

    }
}
