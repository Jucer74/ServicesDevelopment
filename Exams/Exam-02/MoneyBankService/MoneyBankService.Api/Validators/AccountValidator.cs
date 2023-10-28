using FluentValidation;
using MoneyBankService.Api.Dto;
using Org.BouncyCastle.Security;

namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("The AccountType is required.");

        RuleFor(m=> m.CreationDate)
            .Must(BeAValidDate)
            .WithMessage("The CreationDate must be a valid date.");
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("The AccountNumber is required.")
            .MaximumLength(10)
            .WithMessage("The AccountNumber has a maximum length of 10 characters.")
            .Matches(@"\d{10}")
            .WithMessage("The AccountNumber only accepts numbers.");
        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("The OwnerName is required.")
            .MaximumLength(100)
            .WithMessage("The OwnerName has a maximum length of 100 characters.");
        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("The BalanceAmount is required.")
            .GreaterThan(0)
            .WithMessage("The BalanceAmount must be greater than 0.");
        //RuleFor(m => m.OverdraftAmount)
        //    .NotEmpty()
        //    .WithMessage("The OverdraftAmount is required.");


    }

    private static bool BeAValidDate(DateTime date)
    {
        return isValidYear(date.Year) && isValidMonth(date.Month) && isValidDay(date.Day);
    }

    private static bool isValidYear(int year)
    {
        return year >= DateTime.Now.Year;
    }

    private static bool isValidMonth(int month)
    {
        return month >= 1 && month <= 12;
    }

    private static bool isValidDay(int day)
    {
        return day >= 1 && day <= 31;
    }
}
