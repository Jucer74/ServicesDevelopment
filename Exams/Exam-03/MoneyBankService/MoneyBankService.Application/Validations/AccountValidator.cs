using FluentValidation;
using MoneyBankService.Application.Dtos;

namespace MoneyBankService.Application.Validations
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            RuleFor(m => m.AccountType)
                .NotEmpty()
                    .WithMessage("El campo Tipo de Cuenta es Requerido")
                .Must(c => c == 'A' || c == 'C')
                    .WithMessage("El campo Tipo de Cuenta solo permite (A o C)");

            RuleFor(m => m.AccountNumber)
                .NotEmpty()
                    .WithMessage("El campo Numero de la Cuenta es Requerido")
                .MaximumLength(10)
                    .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
                .Matches(@"^\d{10}$")  
                    .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

            RuleFor(m => m.OwnerName)
                .NotEmpty()
                    .WithMessage("El campo Nombre del Propietario es Requerido")
                .MaximumLength(100)
                    .WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");

            RuleFor(m => m.BalanceAmount)
                .NotEmpty()
                .WithMessage("El campo Balance es Requerido")
                .Must(v => decimal.Round(v, 2) == v)
                .WithMessage("El campo Balance debe ser en formato Moneda (0.00)");

            RuleFor(m => m.OverdraftAmount)
                .Must(v => decimal.Round(v, 2) == v)
                    .WithMessage("El campo Sobregiro debe ser en formato Moneda (0.00)");
        }
    }
}
