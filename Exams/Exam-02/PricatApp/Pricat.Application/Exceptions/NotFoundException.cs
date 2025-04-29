using System.Diagnostics.CodeAnalysis;

namespace Pricat.Application.Exceptions
{
    // Excepción que indica que el recurso solicitado no fue encontrado (404).
    // Se usa cuando una entidad no se encuentra en la base de datos o en cualquier otro contexto.
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class NotFoundException : BusinessException
    {
        // Constructor sin parámetros.
        public NotFoundException()
        {
        }

        // Constructor que recibe un mensaje como parámetro.
        public NotFoundException(string message) : base(message)
        {
        }

        // Constructor que recibe un mensaje y una excepción interna.
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
