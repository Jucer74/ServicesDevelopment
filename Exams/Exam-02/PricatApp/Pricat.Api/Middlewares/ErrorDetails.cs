// Importa la biblioteca Newtonsoft.Json para trabajar con serialización JSON
using Newtonsoft.Json;

namespace Pricat.Api.Middleware;

// Clase que representa el formato estándar de respuesta para errores en la API
public class ErrorDetails
{
    // Tipo de error (por ejemplo: "Validation", "ServerError", etc.)
    public string ErrorType { get; set; } = null!;

    // Lista de mensajes de error detallados
    public List<string> Errors { get; set; } = null!;

    // Sobrescribe el método ToString para retornar la representación en formato JSON del error
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
