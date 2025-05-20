using System.Diagnostics.CodeAnalysis;
using MoneyBankService.Application.Exceptions;

namespace MoneyBankService.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundException : BusinessException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
