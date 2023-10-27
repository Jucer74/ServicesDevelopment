using FluentValidation;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // El Id No se Valida
        //RuleFor(m => m.Id)
        //    .NotEmpty()
        //    .WithMessage("El campo Id es Requerido.");
        //    //.GreaterThan(0) // Esta Validacion no se puede dar cuando es un nuevo Registro
        //    //.WithMessage("El campo Id debe ser mayor a cero");

        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("El campo AccountType es Requerido.")
            .Matches(@"[AC]")
            .WithMessage("El campo Tipo de Cuenta solo permite (A o C)");

        RuleFor(m => m.CreationDate)
            .NotEmpty()
            .WithMessage("El campo CreationDate es Requerido.");

        RuleFor(m => m.AccountNumber)
           .NotEmpty()
           .WithMessage("El campo Account number es Requerido.")
           .MaximumLength(10)
           .WithMessage("El campo Account number debe tener máximo 10 carácteres")
           .Matches(@"\d{10}")
            .WithMessage("El campo Account number Solo Acepta Numeros");

        RuleFor(m => m.OwnerName)
           .NotEmpty()
           .WithMessage("El campo Owner name es Requerido.")
           .MaximumLength(100)
           .WithMessage("El campo Owner name debe tener máximo 100 carácteres");

        RuleFor(m => m.BalanceAmount)
           .NotEmpty()
           .WithMessage("El campo Balance amount es Requerido.");
        // El tipo de Dato define que sea Moneda
        //.Matches(@"^\d+.?\d{0,2}$")
        //.WithMessage("El campo Balance amount debe ser en formato Moneda (0.00)");

        //RuleFor(m => m.OverdraftAmount)
        //   .NotEmpty()
        //   .WithMessage("El campo Overdraft amount es Requerido.");
        // El tipo de Dato define que sea Moneda
        //.Matches(@"^\d+.?\d{0,2}$")
        //.WithMessage("El campo Overdraft amount debe ser en formato Moneda (0.00)");
    }
}
