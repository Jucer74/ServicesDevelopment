using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MoneyBankService.Api.Middleware
{
    /// <summary>
    /// Handles exceptions globally
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

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
                    throw;
                }

                // Se asume que tienes una extensión HandleExceptionAsync
                object value = await httpContext.HandleExceptionAsync(ex);
            }
        }
    }
}
