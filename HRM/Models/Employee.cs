namespace HRM.Models
{
    public class Employee
    {
        public ICollection<Salary> Salaries { get; set; }
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public ICollection<Attendance> Attendances { get; set; }
        public DateTime HireDate { get; set; }
        public User User { get; set; }
        public string Status { get; set; } = "Active";
    }
}