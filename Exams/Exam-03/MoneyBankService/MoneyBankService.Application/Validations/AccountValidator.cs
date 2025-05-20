using FluentValidation;
using MoneyBankService.Application.DTO;

namespace MoneyBankService.Application.Validations
{
    public class AccountValidator : AbstractValidator<AccountCreateDto>
    {
        public AccountValidator()
        {
            // Tipo de Cuenta: Requiere A o C
            RuleFor(x => x.AccountType)
                .NotEmpty().WithMessage("El tipo de cuenta es obligatorio.")
                .Must(x => x == 'A' || x == 'C')
                .WithMessage("El tipo de cuenta solo puede ser 'A' (Ahorro) o 'C' (Corriente).");


            // Fecha de creación
            RuleFor(x => x.CreationDate)
                .NotEmpty().WithMessage("La fecha de creación es obligatoria.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de creación no puede ser en el futuro.");

            // Número de cuenta: obligatorio, longitud 10, solo números
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("El número de cuenta es obligatorio.")
                .Length(10).WithMessage("El número de cuenta debe tener exactamente 10 caracteres.")
                .Matches(@"^\d{10}$").WithMessage("El número de cuenta debe contener solo dígitos (10 en total).");

            // Nombre del propietario: obligatorio, máximo 100 caracteres
            RuleFor(x => x.OwnerName)
                .NotEmpty().WithMessage("El nombre del propietario es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del propietario no puede exceder los 100 caracteres.");

            // BalanceAmount: >= 0, con hasta 2 decimales, precisión total 18
            RuleFor(x => x.BalanceAmount)
                .GreaterThanOrEqualTo(0).WithMessage("El balance no puede ser negativo.")
                .PrecisionScale(18, 0,true).WithMessage("El balance debe tener hasta 2 decimales y un máximo de 18 dígitos en total.");

            // OverdraftAmount: >= 0, con hasta 2 decimales, precisión total 18
            RuleFor(x => x.OverdraftAmount)
                .GreaterThanOrEqualTo(0).WithMessage("El sobregiro no puede ser negativo.")
                .PrecisionScale(18, 0, true).WithMessage("El sobregiro debe tener hasta 2 decimales y un máximo de 18 dígitos en total.");
        }
    }
}
