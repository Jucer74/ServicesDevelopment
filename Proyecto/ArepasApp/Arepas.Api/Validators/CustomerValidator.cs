using Arepas.Api.Dtos;
using FluentValidation;

namespace Arepas.Api.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El Campo Id es Requerido");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("El UserEmail es Requerido")
                .EmailAddress().WithMessage("El Campo UserEmail Debe Ser un Email Valido")
                .MaximumLength(250).WithMessage("La Longitud Maxima para el Campo UserEmail es de 250 Caracteres");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El Nombre Completo del Cliente es Requerido")
                .MaximumLength(100).WithMessage("La Longitud Maxima para Nombre Completo del Cliente es de 100 Caracteres");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La Direccion del Cliente es Requerida")
                .MaximumLength(100).WithMessage("La Longitud Maxima para la Direccion del Cliente es de 250 Caracteres");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("El Telefono del Cliente es Requerido")
                .MaximumLength(100).WithMessage("La Longitud Maxima para el Telefono del cliente es de 50 Caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La Contraseña del Cliente es Requerida")
                .MaximumLength(50).WithMessage("La Longitud Maxima para La Contraseña es de 50 Caracteres");
        }
    }
}