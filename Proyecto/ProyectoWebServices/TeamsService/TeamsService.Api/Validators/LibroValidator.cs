using FluentValidation;
using TeamsService.Api.Dtos;

namespace TeamsService.Api.Validators
{
    public class LibroValidator : AbstractValidator<LibroDTO>
    {
        public LibroValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("The AutorId is required.");

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("El título es obligatorio.")
                .MaximumLength(255)
                .WithMessage("La longitud máxima del título es de 255 caracteres.");

            RuleFor(x => x.Imagen)
                .MaximumLength(255)
                .WithMessage("La longitud máxima de la imagen es de 255 caracteres.");

            RuleFor(x => x.Fecha)
                .NotEmpty()
                .WithMessage("La fecha es obligatoria.");

            RuleFor(x => x.Categoria)
                .NotEmpty()
                .WithMessage("La categoría es obligatoria.")
                .MaximumLength(45)
                .WithMessage("La longitud máxima de la categoría es de 45 caracteres.");
        }
    }
}