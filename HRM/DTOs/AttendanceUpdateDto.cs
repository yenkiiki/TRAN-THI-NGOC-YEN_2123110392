namespace HRM.DTOs
{
    public class AttendanceUpdateDto
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Status { get; set; }
    }
}