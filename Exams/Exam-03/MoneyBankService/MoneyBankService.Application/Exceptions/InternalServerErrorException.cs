using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Application.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
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