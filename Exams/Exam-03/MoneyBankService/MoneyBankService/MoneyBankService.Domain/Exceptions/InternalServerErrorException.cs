using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class InternalServerErrorException : BusinessException
{
    public InternalServerErrorException() { }
    public InternalServerErrorException(string msg) : base(msg) { }
    public InternalServerErrorException(string msg, Exception cause) : base(msg, cause) { }
}