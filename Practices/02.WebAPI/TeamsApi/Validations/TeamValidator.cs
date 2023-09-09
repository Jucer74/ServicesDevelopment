using FluentValidation;
using TeamsApi.Dtos;

namespace TeamsApi.Validations;

public class TeamValidator: AbstractValidator<TeamDto>
{
    public TeamValidator()
    {
        RuleFor(m  => m.Name).NotEmpty();
    }
}
