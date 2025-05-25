using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Api.Validators
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            RuleFor(m => m.AccountNumber)
                .NotEmpty()
                .WithMessage("El número de cuenta es requerido")
                .Length(10)
                .WithMessage("El número de cuenta debe tener 10 caracteres")
                .Matches(@"^\d{10}$")
                .WithMessage("El número de cuenta solo debe contener dígitos");

            RuleFor(m => m.OwnerName)
                .NotEmpty()
                .WithMessage("El nombre del propietario es requerido")
                .MaximumLength(100)
                .WithMessage("El nombre no puede exceder 100 caracteres");

            RuleFor(m => m.BalanceAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El balance no puede ser negativo");

            RuleFor(m => m.AccountType)
                .Must(BeValidAccountType)
                .WithMessage("El tipo de cuenta debe ser 'A' (Ahorros) o 'C' (Corriente)");
        }

        private bool BeValidAccountType(char AccountType)
        {
            return AccountType == 'A' || AccountType == 'C';
        }
    }
}