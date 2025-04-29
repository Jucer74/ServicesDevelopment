using FluentValidation;
using Pricat.Application.DTO;

namespace Pricat.Application.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is Required")
            .MaximumLength(50).WithMessage("Description's Max Length is 50 Characters");
        }
    }
}