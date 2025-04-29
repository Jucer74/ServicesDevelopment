using FluentValidation;
using Pricat.Application.Dtos;

namespace Pricat.Application.Validations;

// Validador para la entidad ProductDto
public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        // Regla: la unidad de medida es obligatoria y no debe exceder 20 caracteres
        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit is Required")
            .MaximumLength(20).WithMessage("Unit's Max Length is 20 Characters");

        // Regla: el código EAN es obligatorio y no debe exceder 13 dígitos
        RuleFor(x => x.EanCode)
            .NotEmpty().WithMessage("EanCode is Required")
            .MaximumLength(13).WithMessage("EanCode's Max Length is 13 digits");

        // Regla: la descripción es obligatoria y no debe exceder 50 caracteres
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is Required")
            .MaximumLength(50).WithMessage("Description's Max Length is 50 Characters");
    }
}
