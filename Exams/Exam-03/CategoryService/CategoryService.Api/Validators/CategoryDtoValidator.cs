using CategoryService.Api.Dtos;
using FluentValidation;
namespace CategoryService.Api.Validators;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is Required")
            .MaximumLength(50)
            .WithMessage("Description's Max Length is 50 Characters");
    }
}
