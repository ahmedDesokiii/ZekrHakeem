namespace ZekrHakeem_v11.Models
{
    public class LogPlanData
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? PlanDataId { get; set; }
        [ForeignKey("PlanDataId")]
        public PlanData? PlanData { get; set; }
    }
}
