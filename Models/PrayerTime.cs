namespace ZekrHakeem_v11.Models
{
    public class PrayerTime
    {
        [Key]
        public Guid Id { get; set; }
        public int? Num { get; set; } = 0;
        [Required]
        public string? PrayName { get; set; }
        public string? PrayTime { get; set; }
        public string? EndEpisodeTime { get; set; }

        //public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
