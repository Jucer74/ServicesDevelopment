using FluentValidation;
using PizzeriaApi.Dtos;

namespace PizzeriaApi.Validations;

public class PizzeriaValidator : AbstractValidator<PizzeriaDto>
{
    public PizzeriaValidator()
    {
        RuleFor(r => r.Categoriaa)
            .NotEmpty()
            .WithMessage("The Property Type is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Property Type is 50 characters.");
    }
}
