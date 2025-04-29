using System.Diagnostics.CodeAnalysis;

namespace Pricat.Application.Exceptions
{
    // Excepción que representa un error de servidor interno (500).
    // Se usa para señalar que algo salió mal en el servidor al procesar la solicitud.
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InternalServerErrorException : BusinessException
    {
        // Constructor sin parámetros.
        public InternalServerErrorException()
        {
        }

        // Constructor que recibe un mensaje como parámetro.
        public InternalServerErrorException(string message) : base(message)
        {
        }

        // Constructor que recibe un mensaje y una excepción interna.
        public InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
