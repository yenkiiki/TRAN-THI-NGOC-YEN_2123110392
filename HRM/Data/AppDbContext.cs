using Microsoft.EntityFrameworkCore;
using HRM.Models;

namespace HRM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Nhân viên
        public DbSet<Employee> Employees { get; set; }

        // Phòng ban
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}