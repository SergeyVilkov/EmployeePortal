using EmployeePortal.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Web.Data
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
        {  
        }

        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;

    }
}
