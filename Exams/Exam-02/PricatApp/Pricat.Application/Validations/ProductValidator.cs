using FluentValidation;
using Pricat.Application.DTO;

namespace Pricat.Application.Validations
{
    public class ProductValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductValidator()
        {
            
            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Unit is Required")
                
                .MaximumLength(20).WithMessage("Unit's Max Length is 20 Characters");

            RuleFor(x => x.EanCode)
                .NotEmpty().WithMessage("EanCode is Required")
                .MaximumLength(13).WithMessage("EanCode's Max Length is 13 digits");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is Required")
                .MaximumLength(50).WithMessage("Description's Max Length is 50 Characters");

         
        }
    }
}
