using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            //RuleFor(m => m.Id)
            //    .NotEmpty()
            //    .WithMessage("The Id field is required.");
                //.GreaterThan(0)
                //.WithMessage("The Id field must be greater than zero.");

            RuleFor(m => m.AccountType)
                .NotEmpty()
                .WithMessage("The AccountType field is required.")
                .Matches(@"[AC]")
                .WithMessage("The Account Type field only allows 'A' or 'C'.");

            RuleFor(m => m.CreationDate)
                .NotEmpty()
                .WithMessage("The CreationDate field is required.");

            RuleFor(m => m.AccountNumber)
                .NotEmpty()
                .WithMessage("The Account number field is required.")
                .MaximumLength(10)
                .WithMessage("The Account number field must have a maximum length of 10 characters.")
                .Matches(@"^\d{10}$")
                .WithMessage("The Account number field only accepts 10-digit numbers.");

            RuleFor(m => m.OwnerName)
                .NotEmpty()
                .WithMessage("The Owner name field is required.")
                .MaximumLength(100)
                .WithMessage("The Owner name field must have a maximum length of 100 characters.");

            RuleFor(m => m.BalanceAmount)
                .NotEmpty()
                .WithMessage("The Balance amount field is required.");
                //.Matches(@"^\d+(\.\d{1,2})?$")
                //.WithMessage("The Balance amount field must be in currency format (0.00).");

            //RuleFor(m => m.OverdraftAmount)
            //    .NotEmpty()
            //    .WithMessage("The Overdraft amount field is required.")
            //    .Matches(@"^\d+(\.\d{1,2})?$")
            //    .WithMessage("The Overdraft amount field must be in currency format (0.00).");
        }
    }
}
