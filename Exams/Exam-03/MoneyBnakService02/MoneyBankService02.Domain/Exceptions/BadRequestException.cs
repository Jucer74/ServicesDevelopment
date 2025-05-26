namespace MoneyBankService02.Domain.Exceptions;

public sealed class BadRequestException : BusinessException
{
    public BadRequestException(string errorDetails) : base(errorDetails) { }
    public BadRequestException(string errorDetails, Exception internalError) : base(errorDetails, internalError) { }
}

public sealed class NotFoundException : BusinessException
{
    public NotFoundException(string what) : base(what) { }
    public NotFoundException(string what, Exception why) : base(what, why) { }
}

public sealed class InternalServerErrorException : BusinessException
{
    public InternalServerErrorException(string msg) : base(msg) { }
    public InternalServerErrorException(string msg, Exception cause) : base(msg, cause) { }
}
