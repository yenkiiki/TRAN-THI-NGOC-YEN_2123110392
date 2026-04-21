namespace HRM.DTOs
{
    public class AttendanceResponseDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public double? WorkHours { get; set; }

        // ✨ THÊM DÒNG NÀY VÀO ĐÂY ĐỂ HẾT LỖI
        public string WorkHoursDisplay { get; set; } 

        public string Status { get; set; }
    }
}