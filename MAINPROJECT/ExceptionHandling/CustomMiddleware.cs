namespace MAINPROJECT.ExceptionHandling
{
    public class CustomMiddleware
    {
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly RequestDelegate _next;
        public CustomMiddleware(ILogger<CustomMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
            
        }
        public  async Task  Invoke(HttpContext context) 
            {
            _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context);

            _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);

        }
    }
    
}
