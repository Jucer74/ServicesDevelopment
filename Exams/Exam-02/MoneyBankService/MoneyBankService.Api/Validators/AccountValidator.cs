using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // Validate id is not null
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("The AccountType is required.");

        // Validate creation date
        RuleFor(m => m.CreationDate)
            .Must(BeAValidDate)
            .WithMessage("The CreationDate must be a valid date.");

        // Validate account number
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("The AccountNumber is required.")
            .MaximumLength(10)
            .WithMessage("The AccountNumber has a maximum length of 10 characters.")
            .Matches(@"\d{10}")
            .WithMessage("The AccountNumber only accepts numbers.");

        // Validate owner name
        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("The OwnerName is required.")
            .MaximumLength(100)
            .WithMessage("The OwnerName has a maximum length of 100 characters.");

        // Validate balance amount
        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("The BalanceAmount is required.")
            .GreaterThan(0)
            .WithMessage("The BalanceAmount must be greater than 0.");

        // Validate overdraft amount
        RuleFor(m => m.OverdraftAmount)
            .NotEmpty()
            .WithMessage("The OverdraftAmount is required.");
    }

    // Validate date
    private static bool BeAValidDate(DateTime date)
    {
        return IsValidYear(date.Year) && IsValidMonth(date.Month) && IsValidDay(date.Day);
    }

    // Validate year
    private static bool IsValidYear(int year)
    {
        return year >= DateTime.Now.Year;
    }

    // Validate month
    private static bool IsValidMonth(int month)
    {
        return month is >= 1 and <= 12;
    }

    // Validate day
    private static bool IsValidDay(int day)
    {
        return day is >= 1 and <= 31;
    }
}