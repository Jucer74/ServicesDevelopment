using FluentValidation;
using MoneyBankService.Api.Dto;
using Org.BouncyCastle.Security;

namespace MoneyBankService.Api.Validators;

public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("El campo AccountType es requerido.");


        RuleFor(m => m.CreationDate)
            .Must(BeAValidDate)
            .WithMessage("El campo CreationDate debe ser una fecha valida.");


        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo AccountNumber es requerido.")

            .MaximumLength(10)
            .WithMessage("El campo AccountNumber tiene una longitud maxima de 10 caracteres.")

            .Matches(@"\d{10}")
            .WithMessage("El campo AccountNumber solo acepta numeros.");


        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("El campo OwnerName es requerido.")

            .MaximumLength(100)
            .WithMessage("El campo OwnerName tiene una longitud maxima de 100 caracteres.");


        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("El campo BalanceAmount es requerido.")

            .GreaterThan(0)
            .WithMessage("El campo BalanceAmount debe ser mayor que 0.");


        //RuleFor(m => m.OverdraftAmount)
        //    .NotEmpty()
        //    .WithMessage("El campo OverdraftAmount es requerido.");


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
