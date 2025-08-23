using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Api.Validators
{
    public class TransactionValidator : AbstractValidator<TransactionDto>
    {
        public TransactionValidator()
        {
            RuleFor(transaction => transaction.AccountNumber)
                .NotEmpty().WithMessage("El campo Numero de la Cuenta es Requerido.")
                .Length(10).WithMessage("El campo Numero de La Cuenta debe tener una longitud de 10 caracteres.")
                .Matches(@"^\d{10}$").WithMessage("El campo Numero de la Cuenta Solo Acepta Numeros y debe tener 10 dígitos.");

            RuleFor(transaction => transaction.ValueAmount)
                .NotEmpty().WithMessage("El campo Valor es Requerido.")
                .GreaterThan(0).WithMessage("El campo Valor debe ser mayor a cero.")
                //.ScalePrecision(2, 18).WithMessage("El campo Valor debe tener un máximo de 2 decimales y una precisión adecuada."); 
                .PrecisionScale(18, 2, false).WithMessage("El campo Valor debe tener una precisión máxima de 18 dígitos y 2 decimales."); // (precision, scale, fixPrecision: false)
        }
    }
}