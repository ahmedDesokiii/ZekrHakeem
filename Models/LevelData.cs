namespace ZekrHakeem_v11.Models
{
    public class LevelData
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public int? Num { get; set; }

        public string? Description { get; set; } = " ";

        public Guid? PlanDataId { get; set; } = null;
        [ForeignKey("PlanDataId")]
        public PlanData? PlanData { get; set; }

        public ICollection<LessonData> LessonsData { get; set; } = new List<LessonData>();
        public int? CurrentState { get; set; }

    }
}
