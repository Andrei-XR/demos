using ExampleMultiTenant.Data;
using ExampleMultiTenant.Data.Migration;
using ExampleMultiTenant.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DatabaseMigrator>();

//HABILITAR SOMENTE QUANDO FOR NECESSÁRIO RODAR AS MIGRAÇÕES
//builder.Services.AddDbContext<DynamicDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddDbContext<DynamicDbContext>((serviceProvider, options) =>
{
    // Configura o DbContext dinamicamente usando o contexto da requisição
    var httpContextAcessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var httpContext = httpContextAcessor.HttpContext;

    if (httpContext != null && httpContext.Items.TryGetValue("ConnectionString", out var connectionString))
    {
        options.UseSqlServer(connectionString.ToString());
    }
    else
    {
        throw new InvalidOperationException("Connection string could not be resolved.");
    }
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// middleware personalizado 
app.UseMiddleware<ClientDatabaseMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
