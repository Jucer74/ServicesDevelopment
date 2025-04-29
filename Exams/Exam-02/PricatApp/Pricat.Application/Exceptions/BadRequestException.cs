using System.Diagnostics.CodeAnalysis;

namespace Pricat.Application.Exceptions
{
    // Excepción que representa un error de tipo "Bad Request" (400) en la lógica de negocio.
    // Se utiliza para indicar que la solicitud realizada no es válida.
    [ExcludeFromCodeCoverage] // Se excluye del análisis de cobertura de código.
    [Serializable]
    public class BadRequestException : BusinessException
    {
        // Constructor sin parámetros.
        public BadRequestException()
        {
        }

        // Constructor que recibe un mensaje como parámetro.
        public BadRequestException(string message) : base(message)
        {
        }

        // Constructor que recibe un mensaje y una excepción interna.
        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
