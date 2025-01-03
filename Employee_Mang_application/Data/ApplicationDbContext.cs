using Employee_Mang_application.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Employee_Mang_application.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
