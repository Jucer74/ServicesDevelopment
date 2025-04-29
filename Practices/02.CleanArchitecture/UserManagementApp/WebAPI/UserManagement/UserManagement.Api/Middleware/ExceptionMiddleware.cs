using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Middleware
{
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