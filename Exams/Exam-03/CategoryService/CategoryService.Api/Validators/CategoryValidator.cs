using CategoryService.Api.Dtos;
using FluentValidation;

namespace CategoryService.Api.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is Required")
                .MaximumLength(50)
                .WithMessage("Description's Max Length is 50 Characters");
        }
    }
}