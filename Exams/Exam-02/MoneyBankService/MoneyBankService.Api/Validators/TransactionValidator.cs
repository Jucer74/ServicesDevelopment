using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{

    public TransactionValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("El campo id de la Cuenta es Requerido");

        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Valor es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}")
            .WithMessage("Este campo solo acepta numeros");

        RuleFor(m => m.ValueAmount)
            .NotEmpty()
            .WithMessage("Este campo es requerido")
            .GreaterThan(0)
            .WithMessage("El valor debe ser mayor que 0");

    }


}
