using System;

namespace MoneyBankService.Domain.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
    public BusinessException(string message, Exception inner) : base(message, inner) { }
}