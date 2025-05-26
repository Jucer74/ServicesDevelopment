namespace MoneyBankService02.Domain.Exceptions;

public abstract class BusinessException : Exception
{
    protected BusinessException() { }

    protected BusinessException(string errorMessage) : base(errorMessage) { }

    protected BusinessException(string errorMessage, Exception innerError)
        : base(errorMessage, innerError) { }
}
