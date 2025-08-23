using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class BadRequestException : BusinessException
{
    public BadRequestException()
    {
    }

    public BadRequestException(string errorDetails)
        : base(errorDetails)
    {
    }

    public BadRequestException(string errorDetails, Exception internalError)
        : base(errorDetails, internalError)
    {
    }
}