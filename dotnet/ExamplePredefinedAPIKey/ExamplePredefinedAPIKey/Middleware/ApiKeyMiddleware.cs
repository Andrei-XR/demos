namespace ExamplePredefinedAPIKey.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        private const string ApiKeyHeaderName = "X-Api-Key";

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string predefinedApiKey = _configuration.GetSection("ApiKey").Value ?? "";

            if (string.IsNullOrWhiteSpace(predefinedApiKey))
            {
                context.Response.StatusCode = 501; //server error
                await context.Response.WriteAsync("API KEY definition problem");
                return;
            }

            if(!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401; //unauthorized
                await context.Response.WriteAsync("API KEY is missing");
                return;
            }

            if(!extractedApiKey.Equals(predefinedApiKey.ToString()))
            {
                context.Response.StatusCode = 403; //forbidden
                await context.Response.WriteAsync("Invalid API KEY");
                return;
            }

            await _next(context);
        }
    }
}
