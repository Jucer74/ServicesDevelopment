using FluentValidation;
using Pricat.Application.Dtos;


namespace Pricat.Application.Validations;

public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {

        RuleFor(m => m.Description)
            .MaximumLength(50)
            .WithMessage("The maximum length of Description is 50 characters.");
    }
}