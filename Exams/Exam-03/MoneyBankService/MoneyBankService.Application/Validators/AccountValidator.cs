using FluentValidation;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // Tipo de cuenta: requerido, solo A o C
        RuleFor(x => x.AccountType)
            .NotEmpty().WithMessage("El campo Tipo de Cuenta es Requerido")
            .Must(t => t == 'A' || t == 'C')
            .WithMessage("El campo Tipo de Cuenta solo permite (A o C)");

        // Fecha de creación: requerida (en general DateTime no puede ser null, pero se puede validar lógica si hace falta)
        RuleFor(x => x.CreationDate)
            .NotEmpty().WithMessage("La Fecha de Creación es Requerida");

        // Número de cuenta: requerido, máx 10, solo dígitos
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("El campo Numero de la Cuenta es Requerido")
            .MaximumLength(10).WithMessage("El campo Numero de La Cuenta tiene una longitud maxima de 10 caracteres")
            .Matches(@"^\d{10}$").WithMessage("El Campo Numero de la Cuenta Solo Acepta Numeros");

        // Nombre del propietario: requerido, máx 100
        RuleFor(x => x.OwnerName)
            .NotEmpty().WithMessage("El campo Nombre del Propietario es Requerido")
            .MaximumLength(100).WithMessage("El campo Nombre del Propietario tiene una longitud maxima de 100 caracteres");

        // Balance: requerido, formato moneda
        RuleFor(x => x.BalanceAmount)
            .NotNull().WithMessage("El campo Balance es Requerido")
            .InclusiveBetween(1, 999999999999999999.99m).WithMessage("El campo Balance debe ser en formato Moneda (0.00) y ser mayor a 0.00");

        // Sobregiro: requerido, formato moneda
        RuleFor(x => x.OverdraftAmount)
            .NotNull().WithMessage("El campo Sobregiro es Requerido")
            .InclusiveBetween(0, 999999999999999999.99m).WithMessage("El campo Sobregiro debe ser en formato Moneda (0.00)");
    }
}
