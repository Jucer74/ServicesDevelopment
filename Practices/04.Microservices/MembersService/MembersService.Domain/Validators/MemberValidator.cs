using FluentValidation;
using MembersService.Domain.Dtos;

namespace MembersService.Domain.Validators;

public class MemberValidator:AbstractValidator<MemberDto>
{
    public MemberValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("The FirstName is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of FirstName is 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("The LastName is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of LastName is 50 characters.");

        RuleFor(x => x.Position)
            .MaximumLength(20)
            .WithMessage("The maximum length of Position is 20 characters.");

        RuleFor(x => x.TeamId)
            .NotEmpty()
            .WithMessage("The TeamId is required.");
    }
}
