namespace ZekrHakeem_v11.Models
{
    public class EpisodeType
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string? Name { get; set; }
        public string? EpisodePicture { get; set; }
        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        public int? CurrentState { get; set; }
    }
}
