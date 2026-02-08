namespace ZekrHakeem_v11.Models
{
    public class LogCenter
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid UserId { get; set; }

        public Guid? CenterId { get; set; }
        [ForeignKey("CenterId")]
        public Center? Center { get; set; }
    }
}
