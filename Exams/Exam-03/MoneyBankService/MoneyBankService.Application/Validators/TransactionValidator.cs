using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(transaction => transaction.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"^\d{10}$")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(transaction => transaction.ValueAmount)
            .NotEmpty()
            .WithMessage("El campo Valor es Requerido")
            .Must(balance => decimal.Round(balance, 2) == balance)
            .WithMessage("El campo Balance debe ser en formato Moneda (0.00)");
    }
}