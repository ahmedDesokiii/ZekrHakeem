namespace ZekrHakeem_v11.Models
{
    public class LogEpisodeProg
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid? UserId { get; set; }

        public Guid? EpisodeProgId { get; set; }
        [ForeignKey("EpisodeProgId")]
        public EpisodeProg? EpisodeProg { get; set; }
    }
}
