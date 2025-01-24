using ExampleMultiTenant.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleMultiTenant.Data
{
    public class MigrationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MigrationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                //string de conexão fixa para migrações
                var connectionString = _configuration.GetConnectionString("MigrationConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
