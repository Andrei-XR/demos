using ExampleApiKey.Data;
using Microsoft.EntityFrameworkCore;

namespace ExampleApiKey.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderNamer = "X-Api-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, EnterpriseDbContext dbContext)
        {
            var path = context.Request.Path.Value;

            if (path.Contains("/api/Users/register"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderNamer, out var extractedApiKey) || string.IsNullOrWhiteSpace(extractedApiKey)) 
            {
                context.Response.StatusCode = 401; //unauthorized
                await context.Response.WriteAsync("API Key is missing");

                return;
            }

            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ApiKey == extractedApiKey.ToString());

            if(user == null)
            {
                context.Response.StatusCode = 404; //forbidden
                await context.Response.WriteAsync("invalid API Key");

                return;
            }

            await _next(context);
        }
    }
}
