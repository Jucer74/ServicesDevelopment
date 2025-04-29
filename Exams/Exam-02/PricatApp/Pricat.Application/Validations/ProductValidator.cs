using FluentValidation;
using Pricat.Application.Dtos;

namespace Pricat.Application.Validations;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(p => p.CategoryId)
           .NotEmpty()
           .WithMessage("The CategoryId is required.");

        RuleFor(p => p.EanCode)
            .NotEmpty()
            .WithMessage("The EanCode is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of EanCode is 50 characters.");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("The Description is required.")
            .MaximumLength(200)
            .WithMessage("The maximum length of Description is 200 characters.");

        RuleFor(p => p.Unit)
            .NotEmpty()
            .WithMessage("The Unit is required.")
            .MaximumLength(20)
            .WithMessage("The maximum length of Unit is 20 characters.");

        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("The Price is required.")
            .GreaterThan(0)
            .WithMessage("The Price must be greater than 0.");
    }
}