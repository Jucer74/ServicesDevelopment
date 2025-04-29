// Define el espacio de nombres para organizar el middleware
namespace Pricat.Api.Middleware;

// Middleware personalizado para el manejo global de excepciones
public class ExceptionMiddleware
{
    // Delegado que representa el siguiente componente del pipeline HTTP
    private readonly RequestDelegate _next;

    // Constructor que recibe el siguiente middleware en la cadena de ejecución
    public ExceptionMiddleware(RequestDelegate next)
    {
        // Asigna el siguiente middleware, lanza una excepción si es nulo
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    // Método principal del middleware que intercepta y maneja las excepciones
    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Si el contexto HTTP es nulo, finaliza sin hacer nada
        if (httpContext == null)
        {
            return;
        }

        try
        {
            // Intenta ejecutar el siguiente middleware en el pipeline
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // Si la respuesta ya se ha comenzado a enviar al cliente, relanza la excepción
            if (httpContext.Response.HasStarted)
            {
                throw;
            }

            // Si no se ha iniciado la respuesta, maneja la excepción usando un método de extensión
            await httpContext.HandleExceptionAsync(ex);
        }
    }
}
