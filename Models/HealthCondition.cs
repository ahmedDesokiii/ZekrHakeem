namespace ZekrHakeem_v11.Models
{
    public class HealthCondition
    {
        [Key]
        public Guid Id { get; set; }
        public int? Num { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public int? CurrentState { get; set; }

    }
}
