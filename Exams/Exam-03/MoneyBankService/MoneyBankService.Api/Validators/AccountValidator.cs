using FluentValidation;
using MoneyBankService.Application.Dtos;

namespace MoneyBankService.Api.Validators;

public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(x => x.AccountType)
            .Must(c => c == 'A' || c == 'C')
            .WithMessage("El campo Tipo de Cuenta solo permite (A o C)");

        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10).WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("El campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(x => x.OwnerName)
            .NotEmpty().WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100).WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");

        RuleFor(x => x.BalanceAmount)
            .GreaterThanOrEqualTo(0).WithMessage("El Balance debe ser mayor o igual a cero");

        RuleFor(x => x.OverdraftAmount)
            .GreaterThanOrEqualTo(0).WithMessage("El Sobregiro debe ser mayor o igual a cero");

        RuleFor(x => x.CreationDate)
            .NotEmpty().WithMessage("La Fecha de creación es requerida");
    }
}
