namespace ZekrHakeem_v11.Models
{
    public class LogAttendance
    {
        public Guid Id { get; set; }
        public string? Action { get; set; }
        public DateTime? Date { get; set; }
        public Guid UserId { get; set; }

        public Guid? AttendanceId { get; set; }
        [ForeignKey("AttendanceId")]
        public Attendance? Attendance { get; set; }
    }
}
