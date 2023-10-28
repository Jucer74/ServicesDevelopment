using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators
{
    public class TransactionValidator : AbstractValidator<TransactionDto>
    {
        public TransactionValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("The Id field is required.")
                .GreaterThan(0)
                .WithMessage("The Id field must be greater than zero.");

            RuleFor(m => m.AccountNumber)
               .NotEmpty()
               .WithMessage("The Account Number field is required.")
               .MaximumLength(10)
               .WithMessage("The Account Number field must have a maximum length of 10 characters.")
               .Matches(@"^\d{10}$")
               .WithMessage("The Account Number field only accepts 10-digit numbers.");

            RuleFor(m => m.ValueAmount)
               .NotEmpty()
               .WithMessage("The Value Amount field is required.");
               //.Matches(@"^\d+(\.\d{1,2})?$")
               //.WithMessage("The Value Amount field must have a currency format (0.00).");
        }
    }
}
