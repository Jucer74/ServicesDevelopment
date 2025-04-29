using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Pricat.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class NotFoundException : BusinessException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
