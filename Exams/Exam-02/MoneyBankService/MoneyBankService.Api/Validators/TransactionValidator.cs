using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{

    //RuleFor(m => m.FIELD)
    //    .NotEmpty()
    //    .WithMessage("El campo FIELD es Requerido.")
    //    .MaximumLength(50)
    //    .WithMessage("El campo FIELD tiene una longitud maxima de 10 caracteres")
    //    .Matches(@"\d{10}")
    //    .WithMessage("El campo FIELD Solo Acepta Numeros")
    //    .GreaterThan(0)
    //    .WithMessage("El campo FIELD debe ser mayor a cero");



    //RuleFor(m => m.AccountNumber)
    //    .NotEmpty()
    //    .WithMessage("El campo FIELD es Requerido.")
    //    .MaximumLength(50)
    //    .WithMessage("El campo FIELD tiene una longitud maxima de 10 caracteres")
    //    .Matches(@"\d{10}")
    //    .WithMessage("El campo FIELD Solo Acepta Numeros")

    public TransactionValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("El campo id de la Cuenta es Requerido");

        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("El campo Valor es Requerido")
            .MaximumLength(10)
            .WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"\d{10}")
            .WithMessage("Este campo solo acepta numeros");

        RuleFor(m => m.ValueAmount)
            .NotEmpty()
            .WithMessage("Este campo es requerido")
            .GreaterThan(0)
            .WithMessage("El valor debe ser mayor que 0");

    }


}
