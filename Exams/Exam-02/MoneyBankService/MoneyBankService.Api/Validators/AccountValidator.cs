using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            //RuleFor(m => m.Id)
            //    .NotEmpty()
            //    .WithMessage("El campo Id es requerido.")
            //    .GreaterThan(0)
            //    .WithMessage("El campo Id debe ser mayor que cero.");

            RuleFor(m => m.AccountType)
                .NotEmpty()
                .WithMessage("El campo AccountType es requerido.")
                .Matches(@"[AC]")
                .WithMessage("El campo AccountType solo permite 'A' o 'C'.");

            RuleFor(m => m.CreationDate)
                .NotEmpty()
                .WithMessage("El campo CreationDate es requerido.");

            RuleFor(m => m.AccountNumber)
                .NotEmpty()
                .WithMessage("El campo AccountNumber es requerido.")
                .MaximumLength(10)
                .WithMessage("El campo AccountNumber debe tener una longitud máxima de 10 caracteres.");
                //.Must(accountNumber => accountNumber.Length == 10 && accountNumber.All(char.IsDigit))
                //.WithMessage("El campo AccountNumber debe ser una cadena de 10 dígitos.");

            RuleFor(m => m.OwnerName)
                .NotEmpty()
                .WithMessage("El campo OwnerName es requerido.")
                .MaximumLength(100)
                .WithMessage("El campo OwnerName debe tener una longitud máxima de 100 caracteres.");

            RuleFor(m => m.BalanceAmount)
           .NotEmpty()
           .WithMessage("El campo BalanceAmount es requerido.");


           // RuleFor(m => m.OverdraftAmount)
           //.NotEmpty()
           //.WithMessage("El campo OverdraftAmount es requerido.");

        }
    }
}
