using System.Diagnostics.CodeAnalysis;
using MoneyBankService.Domain.Exceptions;

namespace MoneyBankService.Application.Exceptions;

    [ExcludeFromCodeCoverage]
    public class InternalServerErrorException : BusinessException
    {
        // Constructor por defecto
        public InternalServerErrorException()
        {
        }

        // Constructor con mensaje
        public InternalServerErrorException(string message)
            : base(message)
        {
        }

        // Constructor con mensaje y excepción interna
        public InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

