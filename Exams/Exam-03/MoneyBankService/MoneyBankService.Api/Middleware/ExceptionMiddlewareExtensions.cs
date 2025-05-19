public static class ExceptionMiddlewareExtensions
{
    public static async Task<object> HandleExceptionAsync(this HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        // Aquí puedes personalizar tu respuesta
        var result = new
        {
            error = exception.Message,
            details = exception.StackTrace
        };

        await context.Response.WriteAsJsonAsync(result);
        return result;
    }
}
