namespace ZekrHakeem_v11.Models
{
    public class Prog
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Type { get; set; }
        public string? Kind { get; set; }
        public DateTime? StartProg { get; set; }
        public DateTime? EndProg { get; set; }
        public string? Description { get; set; } = " ";
        public string? ApplicationUserId { get; set; } = null;
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<EpisodeProg> EpisodeProgs { get; set; } = new List<EpisodeProg>();

        public Guid? PlanId { get; set; }
        [ForeignKey("PlanId")]
        public PlanData? Plan { get; set; }

        public int? PauseState { get; set; } = 1;
        public int? CurrentState { get; set; }

    }
}
