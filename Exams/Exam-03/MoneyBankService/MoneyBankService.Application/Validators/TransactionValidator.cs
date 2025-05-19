using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

internal class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(transaction => transaction.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Numero de Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"^\d{10}$")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(transaction => transaction.ValueAmount)
            .NotEmpty()
            .WithMessage("El campo Valor es Requerido")
            .Must(amount => decimal.Round(amount, 2) == amount)
            .WithMessage("El campo Valor debe ser en formato Moneda (0.00)");
    }
}