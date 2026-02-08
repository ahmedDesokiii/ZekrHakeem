namespace ZekrHakeem_v11.Models
{
    public class Surah
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = " ";

        public ICollection<Verse> Verses { get; set; } = new List<Verse>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<StudentProg> StudentProgs { get; set; } = new List<StudentProg>();
        public int? CurrentState { get; set; }

    }
}
