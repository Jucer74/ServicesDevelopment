using FluentValidation;
using Product.Api.Dtos;

namespace Product.Api.Validators;

public class ProductValidator: AbstractValidator<ProductDto>
{
    public ProductValidator()
    {

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId is required");

        RuleFor(p => p.CategoryName)
            .NotEmpty()
            .WithMessage("CategoryName is required")
            .MaximumLength(30)
            .WithMessage("The maximum length of CategoryName is 30 characters");

        RuleFor(p=>p.Price)
            .NotEmpty()
            .WithMessage("Price is required");

        RuleFor(p => p.Unit)
            .NotEmpty()
            .WithMessage("Unit is required")
            .MaximumLength(20)
            .WithMessage("Unit's Max Length is 20 Characters");

        RuleFor(p => p.EanCode)
            .NotEmpty()
            .WithMessage("EanCode is required")
            .MaximumLength(13)
            .WithMessage("EanCode's Max Length is 13 digits");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(50)
            .WithMessage("Description's Max Length is 50 Characters");
    }
}
