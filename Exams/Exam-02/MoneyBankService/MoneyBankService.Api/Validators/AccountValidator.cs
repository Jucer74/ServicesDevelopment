using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // TODO: Implement the Validation Rules

        //RuleFor(m => m.FIELD)
        //    .NotEmpty()
        //    .WithMessage("El campo FIELD es Requerido.")
        //    .MaximumLength(50)
        //    .WithMessage("El campo FIELD tiene una longitud maxima de 10 caracteres")
        //    .Matches(@"\d{10}")
        //    .WithMessage("El campo FIELD Solo Acepta Numeros")
        //    .GreaterThan(0)
        //    .WithMessage("El campo FIELD debe ser mayor a cero");

        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("El tipo de cuenta es requerida");

        RuleFor(m => m.CreationDate)
            .Must(BeAValidDate)
            .WithMessage("La fecha de creacion debe ser una fecha valida.");
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("TEl numero de cuenta es requerida")
            .MaximumLength(10)
            .WithMessage("El numero de cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}")
            .WithMessage("El numero de cuenta solo recibe numeros");
        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("El nombre del propietario es requerido")
            .MaximumLength(100)
            .WithMessage("El nombre del propietario tiene una longitud maxima de 10 de caracteres");
        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("El monto del saldo es requerido")
            .GreaterThan(0)
            .WithMessage("The BalanceAmount must be greater than 0.");
        RuleFor(m => m.OverdraftAmount)
            .NotEmpty()
            .WithMessage("El monto del sobregiro es requerido");


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