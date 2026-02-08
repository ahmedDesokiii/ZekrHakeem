namespace ZekrHakeem_v11.Models
{
    public class LogLocation
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location? Location { get; set; }
    }
}
