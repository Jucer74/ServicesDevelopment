using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Application.Exceptions;

/// <summary>
/// Base Business Exception
/// </summary>
[ExcludeFromCodeCoverage]
public class BusinessException : Exception
{
    public BusinessException()
    {
    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}