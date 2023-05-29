using Arepas.Api.Dtos;
using FluentValidation;

namespace Arepas.Api.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El Campo Id es Requerido");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("El UserEmail es Requerido")
                .EmailAddress().WithMessage("El campo UserEmail debe ser un correo valido");

        }
    }
}