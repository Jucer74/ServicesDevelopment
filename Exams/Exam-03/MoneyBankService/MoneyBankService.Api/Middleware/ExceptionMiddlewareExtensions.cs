using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MoneyBankService.Application.Exceptions;
using MoneyBankService.Domain.Exceptions;
using System.Net;

namespace MoneyBankService.Api.Middleware;
public static class ExceptionMiddlewareExtensions
{

    public static ErrorDetails ConstructErrorMessages(this ActionContext context)
    {
        var errors = context.ModelState.Values.Where(v => v.Errors.Count >= 1)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

        var deserializationKeywords = new[] { "invalid start", "unexpected character", "invalid character" };

        var hasDeserializationError = errors.Any(error =>
            deserializationKeywords.Any(keyword =>
                error.Contains(keyword, StringComparison.OrdinalIgnoreCase))
        );

        var filteredErrors = hasDeserializationError
            ? errors.Where(error =>
                    !error.Contains("field is required", StringComparison.OrdinalIgnoreCase))
                .ToList()
            : errors;

        /*return new ErrorDetails
        {
            ErrorType = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            Errors = errors
        };*/

        return new ErrorDetails
        {
            ErrorType = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            Errors = filteredErrors
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
        var nameOfException = exception?.GetType()?.BaseType?.Name ?? string.Empty;

        if (nameOfException.Equals("BusinessException"))
        {
            nameOfException = exception?.GetType()?.Name;
        }

        return nameOfException switch
        {
            // Bad Request
            nameof(BadRequestException) => HttpStatusCode.BadRequest,

            // Not Found
            nameof(NotFoundException) => HttpStatusCode.NotFound,

            // Internal Server Error
            nameof(InternalServerErrorException) => HttpStatusCode.InternalServerError,

            // Default
            _ => HttpStatusCode.InternalServerError
        };
    }
}