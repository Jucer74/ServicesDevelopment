using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        // Validate id is not null
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("The Id is required.");

        // Validate account number 
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("The AccountNumber is required.")
            .MaximumLength(10)
            .WithMessage("The AccountNumber has a maximum length of 10 characters.")
            .Matches(@"\d{10}")
            .WithMessage("The AccountNumber only accepts numbers.");

        // Validate transaction type is D or W
        RuleFor(m => m.ValueAmount)
            .NotEmpty()
            .WithMessage("The ValueAmount is required.")
            .GreaterThan(0)
            .WithMessage("The ValueAmount must be greater than 0.");
    }
}