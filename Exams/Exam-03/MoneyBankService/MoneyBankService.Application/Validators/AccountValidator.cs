using FluentValidation;
using MoneyBankService.Application.DTOs;

namespace MoneyBankService.Application.Validators;

public class AccountValidator : AbstractValidator<AccountDTO>
{
    public AccountValidator()
    {
        RuleFor(account => account.AccountType) // IRuleBuilderInitial<AccountDto,char>
            .NotEmpty()
            .WithMessage("El campo Tipo de Cuenta es Requerido")
            .Must(type => new[] { 'A', 'C' }.Contains(type))
            .WithMessage("\"El campo Tipo de Cuenta solo permite (A o C)");

        RuleFor(account => account.AccountNumber) // IRuleBuilderInitial<AccountDto,string>
            .NotEmpty()
            .WithMessage("El campo Numero de Cuenta es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(expression: @"^\d{10}$")
            .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(account => account.OwnerName) // IRuleBuilderInitial<AccountDto,string>
            .NotEmpty()
            .WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100)
            .WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");

        RuleFor(account => account.BalanceAmount) // IRuleBuilderInitial<AccountDto,decimal>
            .NotEmpty()
            .WithMessage("El campo Balance es Requerido")
            .Must(balance => decimal.Round(balance, 2) == balance)
            .WithMessage("El campo Balance debe ser en formato Moneda (0.00)");
    }
}