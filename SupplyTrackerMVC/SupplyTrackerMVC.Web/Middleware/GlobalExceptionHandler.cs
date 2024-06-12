using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SupplyTrackerMVC.Web.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new 
            {
                //Status = StatusCodes.Status500InternalServerError,
                //Title = "Server error",
                //Detail = $"{exception.Message}\n {exception.InnerException}",

                Message = exception.Message,
                InnerException = exception.InnerException?.Message,
                Type = exception.GetType().FullName,
                StackTrace =  exception.StackTrace 

            };

           // httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
