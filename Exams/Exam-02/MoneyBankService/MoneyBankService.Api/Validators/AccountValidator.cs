using FluentValidation;
using MoneyBankService.Api.Dto;
namespace MoneyBankService.Api.Validators;
public class AccountValidator : AbstractValidator<AccountDto>
{
    private const decimal MAX_OVERDRAFT = 1000000.00M;

    public AccountValidator()
    {
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("El tipo de cuenta es obligatorio")
            .Matches(@"^[AC]$")
            .WithMessage("El tipo de cuenta debe ser 'A' o 'C'.");

        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El número de cuenta es obligatorio")
            .MaximumLength(10)
            .WithMessage("La longitud máxima del número de cuenta es de 10 caracteres")
            .Matches(@"^\d+$")
            .WithMessage("El número de cuenta solo acepta números");

        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("El nombre del propietario es obligatorio")
            .MaximumLength(100)
            .WithMessage("La longitud máxima del nombre del propietario es de 100 caracteres");

        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("El monto de saldo es obligatorio")
            .GreaterThan(0) // Aseguramos que el balance sea positivo
            .WithMessage("El monto de saldo debe ser mayor a cero");

        RuleFor(m => m.OverdraftAmount)
            //.GreaterThan(0) // Aseguramos que el sobregiro sea positivo (si no es cero)
            //.WithMessage("El monto de sobregiro debe ser mayor a cero")
            .LessThanOrEqualTo(MAX_OVERDRAFT)
            .WithMessage($"El monto de sobregiro no puede exceder ${MAX_OVERDRAFT:N2}");
    }
}