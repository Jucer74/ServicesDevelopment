using Arepas.Domain.Exceptions;
using Serilog;

namespace Arepas.Api.Middleware;

/// <summary>
/// Exception Middleware Handler Extension
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Add the Exception Middleware Handler to centralize the error manage
    /// </summary>
    /// <param name="next">Current Http Request Delegate from the Filter and Context</param>
    /// <exception cref="ArgumentNullException">Throw when is not defined the delegate</exception>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    /// <summary>
    /// Async method to get the Current Http Context
    /// </summary>
    /// <param name="httpContext">Current Http Context</param>
    /// <returns>The Current Request Delegate</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            return;
        }

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            if (httpContext.Response.HasStarted)
            {
                Log.Information("The response has already started.");
                throw;
            }

            var logMessageException = $"Exception Not Controlled: {ex.Message}";

            if (ex.GetBaseException() is BusinessException)
            {
                logMessageException = $"BusinessException Controled: {ex.Message}";
            }

            Log.Error(logMessageException, ex);

            await httpContext.HandleExceptionAsync(ex);
        }
    }
}