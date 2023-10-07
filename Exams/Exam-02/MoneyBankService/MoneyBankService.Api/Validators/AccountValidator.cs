using FluentValidation;
using MoneyBankService.Api.Dto;
using Org.BouncyCastle.Security;

namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // Regla de validación para AccountType: No debe estar vacío.
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("The AccountType is required.");

        // Regla de validación para CreationDate: Debe ser una fecha válida.
        RuleFor(m => m.CreationDate)
            .Must(BeAValidDate)
            .WithMessage("The CreationDate must be a valid date.");

        // Regla de validación para AccountNumber:
        // - No debe estar vacío.
        // - Longitud máxima de 10 caracteres.
        // - Debe contener solo números (expresión regular).
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("The AccountNumber is required.")
            .MaximumLength(10)
            .WithMessage("The AccountNumber has a maximum length of 10 characters.")
            .Matches(@"\d{10}")
            .WithMessage("The AccountNumber only accepts numbers.");

        // Regla de validación para OwnerName:
        // - No debe estar vacío.
        // - Longitud máxima de 100 caracteres.
        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("The OwnerName is required.")
            .MaximumLength(100)
            .WithMessage("The OwnerName has a maximum length of 100 characters.");

        // Regla de validación para BalanceAmount:
        // - No debe estar vacío.
        // - Debe ser mayor que 0.
        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("The BalanceAmount is required.")
            .GreaterThan(0)
            .WithMessage("The BalanceAmount must be greater than 0.");

        // Regla de validación para OverdraftAmount: No debe estar vacío.
        RuleFor(m => m.OverdraftAmount)
            .NotEmpty()
            .WithMessage("The OverdraftAmount is required.");
    }

    // Método para validar si una fecha es válida
    private static bool BeAValidDate(DateTime date)
    {
        return isValidYear(date.Year) && isValidMonth(date.Month) && isValidDay(date.Day);
    }

    // Método para validar si el año es válido (mayor o igual al año actual)
    private static bool isValidYear(int year)
    {
        return year >= DateTime.Now.Year;
    }

    // Método para validar si el mes es válido (entre 1 y 12)
    private static bool isValidMonth(int month)
    {
        return month >= 1 && month <= 12;
    }

    // Método para validar si el día es válido (entre 1 y 31)
    private static bool isValidDay(int day)
    {
        return day >= 1 && day <= 31;
    }

}
