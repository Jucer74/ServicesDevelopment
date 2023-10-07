using FluentValidation;
using TeamsService.Api.Dtos;

namespace MembersService.Api.Validators;

public class MemberValidator : AbstractValidator<TeamDto>
{
    public MemberValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .WithMessage("The Team Name is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Team Name is 50 characters.");

        RuleFor(m => m.Coach)
            .MaximumLength(50)
            .WithMessage("The maximum length of Coach Name is 50 characters.");

        RuleFor(m => m.Conference)
            .MaximumLength(20)
            .WithMessage("The maximum length of Conference is 20 characters.");
    }
}