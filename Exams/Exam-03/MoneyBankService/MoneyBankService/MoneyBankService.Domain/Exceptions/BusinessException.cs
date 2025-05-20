using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class BusinessException : Exception
{
    protected BusinessException()
    {
    }

    protected BusinessException(string errorMessage)
        : base(errorMessage)
    {
    }

    protected BusinessException(string errorMessage, Exception innerError)
        : base(errorMessage, innerError)
    {
    }
}