using System.Diagnostics.CodeAnalysis;
using MoneyBankService.Domain.Exceptions;

namespace MoneyBankService.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class BadRequestException : BusinessException
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}