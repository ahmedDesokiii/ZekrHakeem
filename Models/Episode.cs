namespace ZekrHakeem_v11.Models
{
    public class Episode
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "فضلا , ادخل اسم الحلقة بشكل صحيح")]
        [MaxLength(50, ErrorMessage = "اسم الحلقة لا يزيد عن 50 خانة")]
        [MinLength(3, ErrorMessage = "اسم الحلقة لا يقل عن 3 خانات")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "فضلا , حدد نوع الحضور ")]
        public string? AttendType { get; set; }

        [Required(ErrorMessage = "فضلا , حدد نوع اقصي عدد طلاب في الحلقة")]
        public int LimitStudents { get; set; } = 0;
        public bool Status { get; set; } = false;
        public bool ManualFinished { get; set; } = false;
        public bool IsAvilable { get; set; } = false;

        public string? Description { get; set; }
        public DateTime? CheckDate { get; set; }

        [Required(ErrorMessage = "فضلا , حدد موعد الحلقة")]
        public string? Period { get; set; }


        [Required(ErrorMessage = "فضلا , حدد مكان الحلقة")]
        public Guid? EpLocationId { get; set; }
        [ForeignKey("EpLocationId")]
        public Location? EpLocation { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        [Required(ErrorMessage = "فضلا , حدد معلم الحلقة")]
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required(ErrorMessage = "فضلا , حدد نوع الحلقة")]
        public Guid? EpisodeTypeId { get; set; }
        [ForeignKey("EpisodeTypeId")]
        public EpisodeType? EpisodeType { get; set; }

        public int? PauseState { get; set; } = 1;
        public int? CurrentState { get; set; }
    }
}
