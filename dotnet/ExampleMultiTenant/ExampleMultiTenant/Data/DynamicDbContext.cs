using ExampleMultiTenant.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleMultiTenant.Data
{
    public class DynamicDbContext : DbContext
    {
        public DynamicDbContext(DbContextOptions<DynamicDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
