using FluentValidation;
using Pricat.Application.Dtos;

namespace Pricat.Application.Validations;

// Validador para la entidad CategoryDto
public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        // Regla: la descripción no puede estar vacía
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is Required")

            // Regla: la descripción no debe superar los 50 caracteres
            .MaximumLength(50).WithMessage("Description's Max Length is 50 Characters");
    }
}
