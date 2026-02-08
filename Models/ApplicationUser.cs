namespace ZekrHakeem_v11.Models
{
    [Index(nameof(IdentityNum), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(200)]
        public string FullName { get; set; } = string.Empty;
        [Required, MaxLength(200)]
        public string IdentityNum { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? Nationality { get; set; } = string.Empty;
        public string? Adress { get; set; } = string.Empty;
        public byte[]? ProfilePicture { get; set; }
        public string? Notes { get; set; }
        public int? ExperienceYears { get; set; }
        [MaxLength(250)]
        public string? ExperienceDetails { get; set; }

        //public Guid? NationalityId { get; set; }
        //[ForeignKey("NationalityId")]
        //public Nationality? Nationality { get; set; }


        //public List<string>? Roles { get; set; }
        public ICollection<Center>? Centers { get; set; } = new List<Center>();
        public ICollection<Prog>? Progs { get; set; } = new List<Prog>();
        public ICollection<Episode>? Episodes { get; set; } = new List<Episode>();
        public ICollection<EpisodeProg>? EpisodeProgs { get; set; } = new List<EpisodeProg>();
        public ICollection<Student>? Students { get; set; } = new List<Student>();
        public ICollection<Demand>? Demands { get; set; } = new List<Demand>();
        public ICollection<Attendance>? Attendances { get; set; } = new List<Attendance>();


    }
}
