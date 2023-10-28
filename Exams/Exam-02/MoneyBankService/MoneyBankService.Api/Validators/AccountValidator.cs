using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // TODO: Implement the Validation Rules

        //RuleFor(m => m.FIELD)
        //    .NotEmpty()
        //    .WithMessage("El campo FIELD es Requerido.")
        //    .MaximumLength(50)
        //    .WithMessage("El campo FIELD tiene una longitud maxima de 10 caracteres")
        //    .Matches(@"\d{10}")
        //    .WithMessage("El campo FIELD Solo Acepta Numeros")
        //    .GreaterThan(0)
        //    .WithMessage("El campo FIELD debe ser mayor a cero");

    }
}
