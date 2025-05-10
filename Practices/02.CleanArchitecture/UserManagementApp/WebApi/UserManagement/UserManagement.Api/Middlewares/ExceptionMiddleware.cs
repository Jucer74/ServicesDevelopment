using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UserManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Ejecutar el siguiente middleware en el pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Registrar el error
            _logger.LogError(ex, "Error no controlado: {Message}", ex.Message);

            // Manejar la excepción
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        // Manejar diferentes tipos de excepciones
        switch (ex)
        {
            case KeyNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case ArgumentNullException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.GetType().Name switch
            {
                nameof(KeyNotFoundException) => "Recurso no encontrado.",
                nameof(ArgumentNullException) => "Parámetro inválido.",
                _ => "Error interno del servidor."
            },
            Details = ex.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}