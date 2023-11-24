using Microsoft.AspNetCore.Http;
using ProductService.Api.Middleware;
using System;
using System.Threading.Tasks;

namespace ProductService.Api.Middleware
{
    /// <summary>
    /// Handler the exceptions
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
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

                await httpContext.HandleExceptionAsync(ex);
            }
        }
    }
}