using FluentValidation;
using MoneyBankService02.Application.Dto;

namespace MoneyBankService02.Api.Validators;

public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(m => m.AccountNumber)
            .NotEmpty().Length(10).Matches(@"^\d{10}$");

        RuleFor(m => m.OwnerName)
            .NotEmpty().MaximumLength(100);

        RuleFor(m => m.BalanceAmount)
            .GreaterThanOrEqualTo(0);

        RuleFor(m => m.AccountType)
            .Must(accountType => accountType == 'A' || accountType == 'C');
    }
}
