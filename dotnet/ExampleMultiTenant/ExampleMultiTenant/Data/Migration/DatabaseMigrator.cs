using Microsoft.EntityFrameworkCore;

namespace ExampleMultiTenant.Data.Migration
{
    public class DatabaseMigrator
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseMigrator(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateDatabaseAsync(IEnumerable<string> databaseNames)
        {
            foreach (var databaseName in databaseNames)
            {
                //cria string de conexão dinamica
                var connectionString = BuildConnectionString(databaseName)  ;

                //configura dbcontext com a configuração dinamica
                var dbContextOptions = new DbContextOptionsBuilder<DynamicDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                //cria a instancia do dbcontext
                using var context = new DynamicDbContext(dbContextOptions);

                //aplica as migrações
                await context.Database.MigrateAsync();
            }
        }

        private string BuildConnectionString(string databaseName)
        {
            //Parte fixa da conexão
            var serverPart = _configuration.GetConnectionString("DefaultConnection");
            var userId = _configuration["DatabaseConfig:UserId"];
            var password = _configuration["DatabaseConfig:Password"];
            var trustServerCertificate = _configuration["DatabaseConfig:TrustServerCertificate"];

            //concatena a parte dinamica para formar a string de conexão
            return $"{serverPart};Database={databaseName};User Id={userId};Password={password};TrustServerCertificate={trustServerCertificate}";
        }
    }
}
