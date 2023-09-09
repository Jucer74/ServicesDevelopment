using FluentValidation;
using TeamsApi.Dtos;

namespace TeamsApi.Validations;

public class TeamValidator: AbstractValidator<TeamDto>
{
    public TeamValidator()
    {
        RuleFor(m  => m.Name).NotEmpty().WithMessage("El name es requerido")
            .Length(50).WithMessage("La longitud maxima del nombre es 50 car");
        RuleFor(m => m.Coach).NotEmpty().WithMessage("El name es requerido");
    }
}
