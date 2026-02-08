namespace ZekrHakeem_v11.Models
{
    public class Nationality
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string? Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<StudentProg> StudentProgs { get; set; } = new List<StudentProg>();
        //public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
        public int? CurrentState { get; set; }

    }
}
