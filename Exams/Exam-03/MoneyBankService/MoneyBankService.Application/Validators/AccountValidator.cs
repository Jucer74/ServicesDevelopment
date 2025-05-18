using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");
        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100)
            .WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");
        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("El campo Saldo es Requerido")
            .GreaterThanOrEqualTo(0)
            .Must(val => decimal.Round(val, 2) == val)
            .WithMessage("El campo Saldo debe ser en formato Moneda (0.00)");
        RuleFor(m => m.OverdraftAmount)
            .Must(val => decimal.Round(val, 2) == val)
            .WithMessage("El campo Sobregiro debe ser en formato Moneda (0.00)");

    }
}
