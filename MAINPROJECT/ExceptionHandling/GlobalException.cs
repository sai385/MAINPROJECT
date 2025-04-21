using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MAINPROJECT.ExceptionHandling
{
    public class GlobalException : IExceptionHandler
    {
        private readonly ILogger<GlobalException> _logger;
        public GlobalException(ILogger<GlobalException> logger)
        {
            _logger = logger;
            
        }
        public  async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogInformation( "log information error" ,exception.Message);

            var (status, title) = exception switch
            {
                Exception => (StatusCodes.Status400BadRequest, "Bad Request"),
                _ => (StatusCodes.Status500InternalServerError, "server issues")
            };
            var problemdetails = new ProblemDetails
            {
                Status= status,
                Title= title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path,
            };
            httpContext.Response.StatusCode = status;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemdetails, cancellationToken);
            return true;

        }
    }
}
        
