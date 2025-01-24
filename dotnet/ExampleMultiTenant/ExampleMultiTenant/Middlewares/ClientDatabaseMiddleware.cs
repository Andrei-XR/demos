namespace ExampleMultiTenant.Middlewares
{
    public class ClientDatabaseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ClientDatabaseMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica se a rota deve ignorar o cabeçalho (como as rotas de inicialização, migração, etc.)
            var path = context.Request.Path.Value?.ToLower();

            if (path != null && path.Contains("/swagger"))
            {
                await _next(context);
                return;
            }

            // Obtenha o identificador do cliente do cabeçalho (ou outro local)
            if (!context.Request.Headers.TryGetValue("Client-Identifier", out var databaseName))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Client-Identifier header is required.");
                return;
            }

            // Adapte a string de conexão dinamicamente
            var dynamicConnectionString = BuildConnectionString(databaseName.ToString());

            // Armazene a string de conexão no contexto para uso posterior
            context.Items["ConnectionString"] = dynamicConnectionString;

            //prossiga para o próximo middleware
            await _next(context);
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
