namespace HRM.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? EmployeeId { get; set; }
    }
}