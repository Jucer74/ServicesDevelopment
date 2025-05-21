using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validations
{
    public class TransactionValidator : AbstractValidator<TransactionDto>
    {
        public TransactionValidator()
        {
            // Validación del número de cuenta
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("El campo Numero de la Cuenta es Requerido")
                .MaximumLength(10).WithMessage("El campo Numero de La Cuenta tiene una longitud máxima de 10 caracteres")
                .Matches(@"^\d{10}$").WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

            // Validación del valor
            RuleFor(x => x.ValueAmount)
                .GreaterThan(0).WithMessage("El campo Valor debe ser mayor a 0")
                .Must(tieneDosDecimalesComoMaximo)
                .WithMessage("El campo Valor debe tener máximo 2 decimales");
        }

        private bool tieneDosDecimalesComoMaximo(decimal valor)
        {
            return decimal.Round(valor, 2) == valor;
        }
    }
}