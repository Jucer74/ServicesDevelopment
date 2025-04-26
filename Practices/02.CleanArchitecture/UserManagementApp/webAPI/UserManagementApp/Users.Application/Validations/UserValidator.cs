using FluentValidation;
using Users.Application.Dtos;

namespace Users.Application.Validations;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty()
            .WithMessage("The Email is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of Email is 50 characters.");

        RuleFor(m => m.FullName)
            .NotEmpty()
            .WithMessage("The FullName is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of FullName is 50 characters.");

        RuleFor(m => m.UserName)
            .MaximumLength(30)
            .WithMessage("The maximum length of UserName is 30 characters.");
    }
}