namespace ZekrHakeem_v11.Models
{
    public class LogDemand
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? DemandId { get; set; }
        [ForeignKey("DemandId")]
        public Demand? Demand { get; set; }
    }
}
