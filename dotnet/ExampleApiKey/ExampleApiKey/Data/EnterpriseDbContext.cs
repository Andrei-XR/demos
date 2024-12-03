using ExampleApiKey.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApiKey.Data
{
    public class EnterpriseDbContext : DbContext
    {
        public EnterpriseDbContext(DbContextOptions<EnterpriseDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
