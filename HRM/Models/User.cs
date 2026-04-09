namespace HRM.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin / HR / Employee

        // Optional: liên kết Employee
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}