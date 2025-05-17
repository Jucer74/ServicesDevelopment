using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(account => account.AccountType)
            .NotEmpty()
            .WithMessage("El campo Tipo de Cuenta es Requerido")
            .Must(type => new[] { 'A', 'C' }.Contains(type))
            .WithMessage("El campo Tipo de Cuenta solo permite (A o C)");

        RuleFor(account => account.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"^\d{10}$")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(account => account.OwnerName)
            .NotEmpty()
            .WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100)
            .WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");

        RuleFor(account => account.BalanceAmount)
            .NotEmpty()
            .WithMessage("El campo Balance es Requerido")
            /*.Matches(@"^\d+.?\d{0,2}$")*/
            .GreaterThan(0)
            .WithMessage("El campo Balance debe ser mayor a cero")
            .Must(balance => decimal.Round(balance,2) == balance)
            .WithMessage("El campo Balance debe ser en formato Moneda (0.00)");
        
        RuleFor(account => account.OverdraftAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El campo Sobregiro es Requerido")
            .Must(balance => decimal.Round(balance,2) == balance)
            .WithMessage("El campo Sobregiro debe ser en formato Moneda (0.00)");
        
        

    }
}
