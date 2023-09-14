using FluentValidation;
using TeamsApi.Dtos;

namespace TeamsApi.Validations;

public class TeamMemberValidator : AbstractValidator<TeamMemberDto>
{
    public TeamMemberValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty()
            .WithMessage("The FirstName is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of FisrtName is 50 characters.");

        RuleFor(m => m.LastName)
            .NotEmpty()
            .WithMessage("The LastName is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of LastName is 50 characters.");

        RuleFor(m => m.Position)
            .MaximumLength(20)
            .WithMessage("The maximum length of Position is 20 characters.");

        RuleFor(m => m.TeamId)
            .NotEmpty()
            .WithMessage("The TeamId is required.");
    }
}