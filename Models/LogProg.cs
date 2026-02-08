namespace ZekrHakeem_v11.Models
{
    public class LogProg
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid UserId { get; set; }

        public Guid? ProgId { get; set; }
        [ForeignKey("ProgId")]
        public Prog? Prog { get; set; }
    }
}
