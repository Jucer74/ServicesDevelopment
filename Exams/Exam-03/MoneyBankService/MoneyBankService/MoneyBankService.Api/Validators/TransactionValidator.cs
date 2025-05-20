using FluentValidation;
using MoneyBankService.Application.Dto;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("Debe ingresar el número de cuenta")
            .Length(10).WithMessage("El número de cuenta debe tener exactamente 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("Solo se permiten dígitos numéricos");

        RuleFor(x => x.ValueAmount)
            .GreaterThan(0).WithMessage("El valor debe ser mayor que cero")
            .PrecisionScale(18, 2, false).WithMessage("Formato monetario inválido (ej. 100.00)");
    }
}