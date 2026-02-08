namespace ZekrHakeem_v11.Models
{
    public class PlanData
    {
        [Key]
        public Guid Id { get; set; }
        public int? Num { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string? Name { get; set; }
        public string? Description { get; set; } = " ";

        public ICollection<LevelData> LevelsData { get; set; } = new List<LevelData>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Prog> Progs { get; set; } = new List<Prog>();
        public int? CurrentState { get; set; }

    }
}
