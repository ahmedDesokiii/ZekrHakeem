namespace ZekrHakeem_v11.ViewModels
{
    public class PlanViewModel
    {
        public List<PlanData>? Plans { get; set; }
        public LevelViewModel Levels { get; set; }
        public List<LogPlanData>? LogPlans { get; set; }
        public PlanData? NewPlan { get; set; }

    }
}
