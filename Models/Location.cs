
namespace ZekrHakeem_v11.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "فضلا , ادخل اسم المسجد بشكل صحيح")]
        [MinLength(3, ErrorMessage = "اسم المسجد لا يقل عن 3 خانات")]
        [MaxLength(50, ErrorMessage = "اسم المسجد لا يزيد عن 50 خانة")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "فضلا , حدد نوع المكان")]
        [MaxLength(10)]
        public string? Type { get; set; }
        public string? TotalStudents { get; set; } = "0";
        public string? Description { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        [Required(ErrorMessage = "فضلا , حدد المركز التابع له المسجد")]
        public Guid? LoCenterId { get; set; }
        [ForeignKey("LoCenterId")]
        public Center? LoCenter { get; set; }

        [Required(ErrorMessage = "فضلا , حدد الحي التابع له المسجد")]
        public Guid? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District? District { get; set; }

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        public ICollection<EpisodeProg> EpisodeProgs { get; set; } = new List<EpisodeProg>();
        public int? PauseState { get; set; } = 1;
        public int? CurrentState { get; set; }


    }
}
