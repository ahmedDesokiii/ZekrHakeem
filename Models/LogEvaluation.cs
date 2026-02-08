namespace ZekrHakeem_v11.Models
{
    public class LogEvaluation
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? EvaluationId { get; set; }
        [ForeignKey("EvaluationId")]
        public Evaluation? Evaluation { get; set; }
    }
}
