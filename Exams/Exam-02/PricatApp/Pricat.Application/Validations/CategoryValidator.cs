using FluentValidation;
using Pricat.Application.Dtos;

namespace Pricat.Application.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Description is Required")
                .MaximumLength(50)
                .WithMessage("Description's Max Length is 50 Characters");
        }
    }
}
