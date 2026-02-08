namespace ZekrHakeem_v11.Models
{
    public class LogStudent
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
    }
}
