using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MoneyBankService02.Domain.Exceptions;
using System.Net;

namespace MoneyBankService02.Api.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static ErrorDetails ConstructErrorMessages(this ActionContext context)
    {
        var errors = context.ModelState.Values
            .Where(v => v.Errors.Count >= 1)
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

        var errors = new List<string> { exception.Message };
        var innerException = exception.InnerException;

        while (innerException != null)
        {
            errors.Add(innerException.Message);
            innerException = innerException.InnerException;
        }

        var errorDetails = new ErrorDetails
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
        var exceptionType = exception?.GetType()?.BaseType?.Name ?? string.Empty;

        if (exceptionType.Equals("BusinessException"))
        {
            exceptionType = exception?.GetType()?.Name;
        }

        return exceptionType switch
        {
            nameof(BadRequestException) => HttpStatusCode.BadRequest,
            nameof(NotFoundException) => HttpStatusCode.NotFound,
            nameof(InternalServerErrorException) => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
