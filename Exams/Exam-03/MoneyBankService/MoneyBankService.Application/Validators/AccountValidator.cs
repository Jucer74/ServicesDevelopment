using FluentValidation;
using MoneyBankService.Application.Dtos;

namespace MoneyBankService.Application.Validators;
public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(a => a.AccountType)
            .Must(type => type == 'A' || type == 'C')
            .WithMessage("El tipo de cuenta solo puede ser 'A' (Ahorros) o 'C' (Corriente)");

        RuleFor(a => a.CreationDate)
            .NotEmpty()
            .WithMessage("La fecha de creación es obligatoria");

        RuleFor(a => a.AccountNumber)
            .NotEmpty().WithMessage("El número de cuenta es obligatorio")
            .Length(10).WithMessage("El número de cuenta debe tener exactamente 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("El número de cuenta debe contener solo números");

        RuleFor(a => a.OwnerName)
            .NotEmpty().WithMessage("El nombre del propietario es obligatorio");

        RuleFor(a => a.BalanceAmount)
            .GreaterThanOrEqualTo(0).WithMessage("El balance no puede ser negativo")
            .ScalePrecision(2, 18).WithMessage("El balance debe tener como máximo 2 decimales");

        RuleFor(a => a.OverdraftAmount)
            .GreaterThanOrEqualTo(0).WithMessage("El monto de sobregiro no puede ser negativo")
            .ScalePrecision(2, 18).WithMessage("El monto de sobregiro debe tener como máximo 2 decimales");
    }
}
