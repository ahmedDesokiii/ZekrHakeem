namespace ZekrHakeem_v11.Models
{
    public class District
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "فضلا , ادخل اسم الحي بشكل صحيح")]
        [MinLength(2, ErrorMessage = "اسم الحي لا يقل عن 2 خانة")]
        [MaxLength(50, ErrorMessage = "اسم الحي لا يزيد عن 50 خانة")]
        public string? Name { get; set; }
        public string? Description { get; set; } = " ";

        //[Required(ErrorMessage = "فضلا , حدد المركز التابع له الحي")]
        //public Guid? CenterId { get; set; }
        //[ForeignKey("CenterId")]
        //public Center? Center { get; set; }

        public ICollection<Location> Locations { get; set; } = new List<Location>();

        public int? CurrentState { get; set; }

    }
}
