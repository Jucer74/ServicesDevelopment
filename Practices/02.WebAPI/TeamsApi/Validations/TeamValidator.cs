using FluentValidation;
using TeamsApi.Dtos;

namespace TeamsApi.Validations;

public class TeamValidator : AbstractValidator<TeamDto>
{
    public TeamValidator()
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