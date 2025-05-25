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
            .NotEmpty().WithMessage("El nombre del propietario es obligatorio")
            .MaximumLength(100).WithMessage("El nombre del propietario  debe tener como máximo 100 caracteres");

        RuleFor(m => m.BalanceAmount)
                .NotEmpty()
                .WithMessage("El campo Balance es Requerido")
                .Must(v => decimal.Round(v, 2) == v)
                .WithMessage("El campo Balance debe ser en formato Moneda (0.00)");

        RuleFor(m => m.OverdraftAmount)
            .Must(v => decimal.Round(v, 2) == v)
                .WithMessage("El campo Sobregiro debe ser en formato Moneda (0.00)");
    }
}
