using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10).WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}").WithMessage("El campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(x => x.ValueAmount)
            .GreaterThan(0).WithMessage("El campo Valor debe ser mayor a cero")
            .Matches(@"^\d+.?\d{0,2}$").WithMessage("El campo Valor debe ser en formato Moneda (0.00)");
    }
}
