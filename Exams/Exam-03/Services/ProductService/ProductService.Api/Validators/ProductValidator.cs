using FluentValidation;
using ProductService.Api.Dtos;

namespace ProductService.Api.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(m => m.CategoryId)
                .NotEmpty()
                .WithMessage("The CategoryId is required.");

            RuleFor(m => m.CategoryName)
                .NotEmpty()
                .WithMessage("The CategoryName is required.")
                .MaximumLength(50)
                .WithMessage("The maximum length of CategoryName is 255 characters.");

            RuleFor(m => m.EanCode)
                .NotEmpty()
                .WithMessage("The EanCode is required.")
                .MaximumLength(50)
                .WithMessage("The maximum length of EanCode is 50 characters.");

            RuleFor(m => m.Description)
                .NotEmpty()
                .WithMessage("The Description is required.")
                .MaximumLength(255)
                .WithMessage("The maximum length of Description is 255 characters.");

            RuleFor(m => m.Unit)
                .NotEmpty()
                .WithMessage("The Unit is required.")
                .MaximumLength(50)
                .WithMessage("The maximum length of Unit is 50 characters.");

            RuleFor(m => m.Price)
                .NotEmpty()
                .WithMessage("The Price is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Price must be greater than or equal to 0.");
        }
    }
}
