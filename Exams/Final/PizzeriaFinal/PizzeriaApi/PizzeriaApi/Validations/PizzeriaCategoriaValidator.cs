using FluentValidation;
using PizzeriaApi.Dtos;

namespace PizzeriaApi.Validations;

public class PizzeriaCategoriaValidator : AbstractValidator<PizzeriaCategoriaDto>
{
    public PizzeriaCategoriaValidator()
    {
        
        RuleFor(r => r.Nombre)
            .NotEmpty()
            .WithMessage("The Address is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Address is 50 characters.");

        RuleFor(r => r.Tamaño)
            .NotEmpty()
            .WithMessage("The Location is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Location is 50 characters.");

        RuleFor(r => r.Precio)
            .NotEmpty()
            .WithMessage("The Price is required.");

        RuleFor(m => m.PizzasId)
        .NotEmpty()
        .WithMessage("The RealestateId is required.");
    }
}
