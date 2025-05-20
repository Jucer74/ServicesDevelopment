using System.Diagnostics.CodeAnalysis;

namespace MoneyBankService.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class BadRequestException : BusinessException
{
    public string ErrorType { get; }
    public string[] Errors { get; }

    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
        ErrorType = "Bad Request";
        Errors = new[] { message };
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorType = "Bad Request";
        Errors = new[] { message };
    }

    public BadRequestException(string errorType, string[] errors)
        : base(string.Join(" | ", errors))
    {
        ErrorType = errorType;
        Errors = errors;
    }
}
