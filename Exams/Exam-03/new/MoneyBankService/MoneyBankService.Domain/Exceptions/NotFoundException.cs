namespace MoneyBankService.Domain.Exceptions;

public class NotFoundException : BusinessException
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception inner) : base(message, inner) { }
}