using FluentValidation;
using ProductService.Api.Dtos;

namespace ProductService.Api.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .WithMessage("CategoryId is Required");

            RuleFor(p => p.CategoryName)
                .NotEmpty()
                .WithMessage("CategoryName is Required")
                .MaximumLength(30)
                .WithMessage("The maximum length of CategoryName is 30 characters");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("Price is Required");

            RuleFor(p => p.Unit)
                .NotEmpty()
                .WithMessage("Unit is Required")
                .MaximumLength(20)
                .WithMessage("Unit's Max Length is 20 Characters");

            RuleFor(p => p.EanCode)
                .NotEmpty()
                .WithMessage("EanCode is Required")
                .MaximumLength(13)
                .WithMessage("EanCode's Max Length is 13 digits");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("Description is Required")
                .MaximumLength(50)
                .WithMessage("Description's Max Length is 50 Characters");
        }
    }
}
