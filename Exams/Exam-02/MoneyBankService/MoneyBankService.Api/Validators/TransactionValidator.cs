using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("El campo Id es reuqerido.");

        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo AccountNumber es requerido.")

            .MaximumLength(10)
            .WithMessage("El campo AccountNumber tiene una longitud maxima de 10 caracteres.")

            .Matches(@"\d{10}")
            .WithMessage("El campo AccountNumber solo acepta numeros.");

        RuleFor(m => m.ValueAmount)
            .NotEmpty()
            .WithMessage("El campo ValueAmount is requerido.")

            .GreaterThan(0)
            .WithMessage("El campo ValueAmount debe ser mayor que 0.");

    }
}
