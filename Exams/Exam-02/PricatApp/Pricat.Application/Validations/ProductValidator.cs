using FluentValidation;
using Pricat.Application.Dtos;

namespace Pricat.Application.Validations
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {

            RuleFor(p => p.EanCode)
                .NotEmpty().WithMessage("EanCode is Required")
                .NotNull().WithMessage("EanCode is Required")
                .MaximumLength(13).WithMessage("EanCode's Max Length is 13 digits");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is Required")
                .NotNull().WithMessage("Description is Required")
                .MaximumLength(50).WithMessage("Description's Max Length is 50 Characters");

            RuleFor(p => p.Unit)
                .NotEmpty().WithMessage("Unit is Required")
                .NotNull().WithMessage("Unit is Required")
                .MaximumLength(20).WithMessage("Unit's Max Length is 20 Characters");
        }
    }
}
