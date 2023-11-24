using FluentValidation;
using PizzeriaApi.Dtos;

namespace PizzeriaApi.Validations;

public class PizzeriaCategoriaValidator : AbstractValidator<PizzeriaCategoriaDto>
{
    public PizzeriaCategoriaValidator()
    {
        
        RuleFor(r => r.Nombre)
            .NotEmpty()
            .WithMessage("The Nombre is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Nombre is 50 characters.");

        RuleFor(r => r.Tamaño)
            .NotEmpty()
            .WithMessage("The Tamaño is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Tamaño is 50 characters.");

        RuleFor(r => r.Precio)
            .NotEmpty()
            .WithMessage("The Precio is required.");

        RuleFor(m => m.PizzasId)
        .NotEmpty()
        .WithMessage("The PizzasId is required.");
    }
}
