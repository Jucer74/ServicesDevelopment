using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MoneyBankService.Application.Exceptions;
using System.Net;

namespace MoneyBankService.Api.Middleware;
public static class ExceptionMiddlewareExtensions
{
    public static Task HandleExceptionAsync(this HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Por defecto, Internal Server Error
        var statusCode = StatusCodes.Status500InternalServerError;
        var errorType = "Internal Server Error";
        var errors = new List<string> { exception.Message };

        // Si la excepción es de validación, cambia a 400
        if (exception is ValidationException valEx)
        {
            statusCode = StatusCodes.Status400BadRequest;
            errorType = "Bad Request";
            errors = valEx.Errors.Select(e => e.ErrorMessage).ToList();

        }
        // Si la excepción es de not found, cambia a 404
        else if (exception is NotFoundException nfEx)
        {
            statusCode = StatusCodes.Status404NotFound;
            errorType = "Not Found";
            errors = new List<string> { nfEx.Message };
        }

        context.Response.StatusCode = statusCode;

        var result = new
        {
            ErrorType = errorType,
            Errors = errors
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
    }
}
