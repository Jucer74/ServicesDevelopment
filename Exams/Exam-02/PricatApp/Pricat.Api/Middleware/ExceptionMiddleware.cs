using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pricat.Utilities.Exceptions;

namespace Pricat.Api.Middleware 
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                switch (ex)
                {
                    case BadRequestException br:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new { error = br.Message });
                        break;
                    case NotFoundException nf:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsJsonAsync(new { error = nf.Message });
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new { error = "Internal server error" });
                        break;
                }
            }
        }
    }
}
