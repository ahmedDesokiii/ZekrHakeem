namespace ZekrHakeem_v11.Models
{
    public class Center
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "ادخل اسم المركز بشكل صحيح")]
        [MaxLength(30, ErrorMessage = "لا يزيد اسم المركز عن 30 خانة")]
        [MinLength(4, ErrorMessage = "لا يقل اسم المركز عن 4 خانات")]
        public string? Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Type { get; set; }
        public string? Description { get; set; } = " ";

        public ICollection<Location> Locations { get; set; } = new List<Location>();
        //public ICollection<District> Districts { get; set; } = new List<District>();

        [Required(ErrorMessage = "حدد مشرف المركز")]
        public string? ApplicationUserId { get; set; } = null;
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public int? PauseState { get; set; } = 1;
        public int? CurrentState { get; set; }

    }
}
