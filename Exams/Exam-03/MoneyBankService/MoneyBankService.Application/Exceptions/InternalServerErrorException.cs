using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class InternalServerErrorException : BusinessException
{
    public InternalServerErrorException()
    {
    }

    public InternalServerErrorException(string message) : base(message)
    {
    }

    public InternalServerErrorException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}