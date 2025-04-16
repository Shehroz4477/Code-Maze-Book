using System.Net;
using Contract.Interfaces;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;

namespace CompanyEmployees;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILoggerManager _logger;
    public GlobalExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var httpContextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        if (httpContextFeature != null) 
        {
            _logger.LogError($"Something went wrong {exception.Message}");

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                SttausCode = httpContext.Response.StatusCode,
                Message    = "Internal Server Error."
            }.ToString());
        }
        return true;
    }
}
