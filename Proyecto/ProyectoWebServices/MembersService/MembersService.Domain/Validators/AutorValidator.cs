using FluentValidation;
using MembersService.Application.Dtos;
using MembersService.Domain.Dtos;

namespace MembersService.Domain.Validators
{
    public class AutorValidator : AbstractValidator<AutorDTO>
    {
        public AutorValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("The AutorId is required.");

            RuleFor(x => x.LibroId)
                .NotEmpty()
                .WithMessage("The LibroId is required.");

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("The Nombre is required.")
                .MaximumLength(255)
                .WithMessage("The maximum length of Nombre is 255 characters.");

            RuleFor(x => x.Apellido)
                .NotEmpty()
                .WithMessage("The Apellido is required.")
                .MaximumLength(255)
                .WithMessage("The maximum length of Apellido is 255 characters.");

            RuleFor(x => x.Pais)
                .MaximumLength(45)
                .WithMessage("The maximum length of Pais is 45 characters.");
        }
    }
}
