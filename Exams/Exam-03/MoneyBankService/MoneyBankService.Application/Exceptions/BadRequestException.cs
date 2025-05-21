using System.Diagnostics.CodeAnalysis;
using MoneyBankService.Application.Exceptions;

namespace MoneyBankService.Domain.Exceptions;

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