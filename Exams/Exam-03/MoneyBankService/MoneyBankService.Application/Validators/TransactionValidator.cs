using FluentValidation;
using MoneyBankService.Application.Dtos;

namespace MoneyBankService.Application.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(t => t.AccountNumber)
            .NotEmpty().WithMessage("El número de cuenta es obligatorio")
            .Length(10).WithMessage("El número de cuenta debe tener exactamente 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("El número de cuenta debe contener solo números");

        RuleFor(t => t.ValueAmount)
            .NotEqual(0).WithMessage("El valor de la transacción no puede ser cero")
            .ScalePrecision(2, 18).WithMessage("El valor debe tener máximo 2 decimales");
    }
}