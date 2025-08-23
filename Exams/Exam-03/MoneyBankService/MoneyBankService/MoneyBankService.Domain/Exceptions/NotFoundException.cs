using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class NotFoundException : BusinessException
{
    public NotFoundException() { }
    public NotFoundException(string what) : base(what) { }
    public NotFoundException(string what, Exception why) : base(what, why) { }
}