using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
           
           
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("El campo Tipo de Cuenta es Requerido")

            .Matches(@"[AC]")
            .WithMessage("El campo FIELD Solo Acepta Numeros");

         RuleFor(m => m.AccountNumber)
             .NotEmpty()
             .WithMessage("El campo Numero de la Cuenta es Requerido")
             .MaximumLength(10)
             .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
             .Matches(@"\d{10}")
             .WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100)
            .WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");

        

        const decimal MAX_OVERDRAFT = 1000; // Definir el valor adecuado

        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("El monto de saldo es obligatorio")
            .Must(amount => Decimal.TryParse(amount.ToString(), out _))
            .WithMessage("El monto de saldo debe estar en formato de dinero (0.00)");

        RuleFor(m => m.OverdraftAmount)
            .Must(amount => Decimal.TryParse(amount.ToString(), out _))
            .WithMessage("El monto de sobregiro debe estar en formato de dinero (0.00)")
            .LessThanOrEqualTo(MAX_OVERDRAFT)
            .WithMessage($"El monto de sobregiro no puede exceder ${MAX_OVERDRAFT:N2}");



    }
}
