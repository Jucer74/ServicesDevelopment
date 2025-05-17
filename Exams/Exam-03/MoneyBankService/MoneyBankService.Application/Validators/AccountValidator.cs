using FluentValidation;

using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100)
            .WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");
            
        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("El campo Tipo de Cuenta es Requerido.")
            .Must(c => c == 'A' || c == 'C')
            .WithMessage("El campo Tipo de Cuenta solo permite (A o C)");

        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("El campo Balance es requerido.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("El campo Balance solo acepta números positivos.")
            .Must(val => decimal.Round(val, 2) == val)
            .WithMessage("El campo Balance debe ser en formato Moneda (0.00).");

        RuleFor(m => m.OverdraftAmount)
            .Must(val => decimal.Round(val, 2) == val)
            .WithMessage("El campo Sobregiro debe ser en formato Moneda (0.00)");




    }
}
