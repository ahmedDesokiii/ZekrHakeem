namespace ZekrHakeem_v11.Models
{
    public class LogLevelData
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? LevelDataId { get; set; }
        [ForeignKey("LevelDataId")]
        public LevelData? LevelData { get; set; }
    }
}
