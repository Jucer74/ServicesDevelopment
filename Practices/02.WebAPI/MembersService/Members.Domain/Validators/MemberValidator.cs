using FluentValidation;
using Members.Domain.Dtos;

namespace Members.Domain.Validators;

public class MemberValidator:AbstractValidator<MemberDto>
{
    public MemberValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("The maximum length of FisrtName is 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("The maximum length of FisrtName is 50 characters");

        RuleFor(x => x.Position)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("The maximum length of FisrtName is 50 characters");

        RuleFor(x => x.TeamId)
            .NotEmpty()
            .WithMessage("First name is required");
    }
}
