using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(m =>m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");
        RuleFor(m => m.ValueAmount)
            .NotEmpty()
            .WithMessage("El campo Valor es Requerido")
            .Must(val=>decimal.Round(val,2) == val)
            .WithMessage("El campo Valor debe ser en formato Moneda (0.00)");
    }
}
