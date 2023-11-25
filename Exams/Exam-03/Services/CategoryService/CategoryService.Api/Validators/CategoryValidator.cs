using FluentValidation;
using CategoryService.Api.Dtos;

namespace CategoryService.Api.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage("The Description is required.")
                .MaximumLength(50)
                .WithMessage("The maximum length of Description is 50 characters.");
        }
    }
}
