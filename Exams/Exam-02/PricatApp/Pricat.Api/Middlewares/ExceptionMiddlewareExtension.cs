// Definición de la clase estática para las extensiones del middleware de excepciones
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Pricat.Application.Exceptions;
using System.Net;

namespace Pricat.Api.Middleware;

public static class ExceptionMiddlewareExtensions
{
    // Método de extensión para construir mensajes de error a partir de ModelState
    public static ErrorDetails ConstructErrorMessages(this ActionContext context)
    {
        // Obtiene los errores del modelo que tienen al menos un error
        var errors = context.ModelState.Values.Where(v => v.Errors.Count >= 1)
                .SelectMany(v => v.Errors)  // Convierte todos los errores en una lista plana
                .Select(v => v.ErrorMessage)  // Extrae el mensaje de error
                .ToList();

        // Verifica si hay errores de deserialización específicos
        var hasDeserializationError = errors.Any(e =>
                    e.Contains("invalid start", StringComparison.OrdinalIgnoreCase) ||
                    e.Contains("unexpected character", StringComparison.OrdinalIgnoreCase) ||
                    e.Contains("invalid character", StringComparison.OrdinalIgnoreCase)
        );

        // Si hay errores de deserialización, filtra ciertos errores
        var filteredErrors = hasDeserializationError
            ? errors.Where(e => !e.Contains("field is required", StringComparison.OrdinalIgnoreCase)).ToList()
            : errors;

        // Retorna un objeto ErrorDetails con el tipo de error y los errores filtrados
        return new ErrorDetails
        {
            ErrorType = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            Errors = filteredErrors
        };
    }

    // Método de extensión para manejar excepciones y construir la respuesta de error
    public static Task HandleExceptionAsync(this HttpContext context, Exception exception)
    {
        // Obtiene el código de estado HTTP adecuado basado en la excepción
        var httpStatusCode = GetStatusResponse(exception);
        context.Response.ContentType = "application/json";  // Define el tipo de contenido de la respuesta
        context.Response.StatusCode = (int)httpStatusCode;  // Asigna el código de estado HTTP

        // Prepara una lista de errores con el mensaje de la excepción
        var errors = new List<string>() { exception.Message };
        var innerException = exception;

        // Agrega mensajes de excepciones internas, si existen
        do
        {
            innerException = innerException.InnerException;
            if (innerException != null)
            {
                errors.Add(innerException.Message);
            }
        }
        while (innerException != null);

        // Crea un objeto ErrorDetails con el tipo de error y los mensajes
        var errorDetails = new ErrorDetails()
        {
            ErrorType = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode),
            Errors = errors
        };

        // Escribe la respuesta con el objeto ErrorDetails convertido a JSON
        return context.Response.WriteAsync(errorDetails.ToString());
    }

    // Método de extensión para registrar el middleware de excepciones en el pipeline
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();  // Usa el middleware definido anteriormente
    }

    // Método privado para obtener el código de estado HTTP basado en el tipo de excepción
    private static HttpStatusCode GetStatusResponse(Exception exception)
    {
        // Obtiene el nombre de la excepción base, o el nombre de la excepción específica
        var nameOfException = exception?.GetType()?.BaseType?.Name ?? string.Empty;

        // Si es una BusinessException, toma el nombre de la excepción específica
        if (nameOfException.Equals("BusinessException"))
        {
            nameOfException = exception?.GetType()?.Name;
        }

        // Asigna un código de estado HTTP basado en el tipo de excepción
        return nameOfException switch
        {
            nameof(BadRequestException) => HttpStatusCode.BadRequest,  // BadRequestException -> 400
            nameof(NotFoundException) => HttpStatusCode.NotFound,      // NotFoundException -> 404
            nameof(InternalServerErrorException) => HttpStatusCode.InternalServerError,  // 500
            _ => HttpStatusCode.InternalServerError                    // Para cualquier otro tipo de excepción, 500
        };
    }
}
