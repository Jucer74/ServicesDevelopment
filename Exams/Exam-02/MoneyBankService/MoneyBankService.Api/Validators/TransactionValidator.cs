using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class TransactionValidator:AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("El campo Id es Requerido.")
            .GreaterThan(0)
            .WithMessage("El campo Id debe ser mayor a cero");

        RuleFor(m => m.AccountNumber)
           .NotEmpty()
           .WithMessage("El campo Account number es Requerido.")
           .MaximumLength(10)
           .WithMessage("El campo Account number debe tener máximo 10 carácteres")
           .Matches(@"\d{10}")
            .WithMessage("El campo Account number Solo Acepta Numeros");

        RuleFor(m => m.ValueAmount)
           .NotEmpty()
           .WithMessage("El campo Value amount amount es Requerido.")
           .Matches(@"^\d+.?\d{0,2}$")
           .WithMessage("El campo Value amount debe ser en formato Moneda (0.00)");

    }

}
