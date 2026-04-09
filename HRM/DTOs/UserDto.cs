namespace HRM.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
