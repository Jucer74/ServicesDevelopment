using System.Diagnostics.CodeAnalysis;

namespace Pricat.Application.Exceptions
{
    // Clase base para todas las excepciones de negocio en la aplicación.
    // Se utiliza como clase base para manejar errores relacionados con las reglas de negocio.
    // La clase está marcada con [ExcludeFromCodeCoverage] para excluirla del análisis de cobertura de código.
    [ExcludeFromCodeCoverage]
    [Serializable] // Permite que la excepción sea serializable, lo cual es importante para transmitir la excepción entre diferentes dominios.
    public class BusinessException : Exception
    {
        // Constructor sin parámetros.
        public BusinessException()
        {
        }

        // Constructor que recibe un mensaje como parámetro.
        // El mensaje proporciona detalles adicionales sobre el error.
        public BusinessException(string message) : base(message)
        {
        }

        // Constructor que recibe un mensaje y una excepción interna.
        // La excepción interna ayuda a rastrear las excepciones originales que causaron el problema.
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
