using ExamplePredefinedAPIKey.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamplePredefinedAPIKey.Data
{
    public class EnterpriseDbContext : DbContext
    {
        public EnterpriseDbContext(DbContextOptions<EnterpriseDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
    }
}
