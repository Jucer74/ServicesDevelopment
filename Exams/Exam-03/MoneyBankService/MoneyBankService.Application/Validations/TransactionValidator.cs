using FluentValidation;
using MoneyBankService.Application.Dtos;

namespace MoneyBankService.Application.Validations
{
    public class TransactionValidator : AbstractValidator<TransactionDto>
    {
        public TransactionValidator()
        {
            RuleFor(m => m.AccountNumber)
                .NotEmpty()
                    .WithMessage("El campo Numero de la Cuenta es Requerido")
                .MaximumLength(10)
                    .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
                .Matches(@"^\d{10}$")
                    .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

            RuleFor(m => m.ValueAmount)
              .NotEmpty()
                .WithMessage("El campo Valor es Requerido")
              .Must(v => decimal.Round(v, 2) == v)
                .WithMessage("El campo Balance debe ser en formato Moneda (0.00)");
        }
    }
}
