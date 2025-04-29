using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Pricat.Application.Execptions;
using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pricat.Api.Middleware
{
    public static class ExceptionExtension
    {
        public static ErrorDetails ConstructErrorMessages(this ActionContext context)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count >= 1)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

            return new ErrorDetails
            {
                ErrorType = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                Errors = errors
            };
        }

        public static Task HandleExceptionAsync(this HttpContext context, Exception exception)
        {
            var httpStatusCode = GetStatusResponse(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            var errors = new List<string>() { exception.Message };
            var innerException = exception;
            do
            {
                innerException = innerException.InnerException;
                if (innerException != null)
                {
                    errors.Add(innerException.Message);
                }
            }
            while (innerException != null);

            var errorDetails = new ErrorDetails()
            {
                ErrorType = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode),
                Errors = errors
            };

            return context.Response.WriteAsync(errorDetails.ToString());
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        private static HttpStatusCode GetStatusResponse(Exception exception)
        {
            var nameOfException = exception?.GetType()?.BaseType?.Name ?? String.Empty;

            if (nameOfException.Equals("BusinessException"))
            {
                nameOfException = exception?.GetType()?.Name;
            }

            return nameOfException switch
            {
                nameof(BadRequestException) => HttpStatusCode.BadRequest,

                nameof(NotFoundException) => HttpStatusCode.NotFound,

                nameof(InternalServerErrorException) => HttpStatusCode.InternalServerError,

                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}