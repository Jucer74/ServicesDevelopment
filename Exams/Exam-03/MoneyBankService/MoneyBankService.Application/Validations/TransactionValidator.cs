using FluentValidation;
using MoneyBankService.Application.DTO;

namespace MoneyBankService.Application.Validations
{
    public class TransactionValidator : AbstractValidator<TransactionCreateDto>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("El número de cuenta es obligatorio.")
                .Length(10).WithMessage("El número de cuenta debe tener exactamente 10 caracteres.")
                .Matches(@"^\d{10}$").WithMessage("El número de cuenta debe contener solo dígitos.");

            RuleFor(x => x.ValueAmount)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.")
                .PrecisionScale(18, 2, true).WithMessage("El monto debe tener hasta 2 decimales.");
        }
    }
}
