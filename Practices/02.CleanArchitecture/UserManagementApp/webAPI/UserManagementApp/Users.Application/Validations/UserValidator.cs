using FluentValidation;
using Users.Application.Dtos.Users;

namespace Users.Application.Validations;

public class UserValidator : AbstractValidator<UserDtoInput>
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

        RuleFor(m => m.Password).NotEmpty()
            .WithMessage("The Password is required.")
            .MinimumLength(6)
            .WithMessage("The minimum length of Password is 6 characters.")
            .MaximumLength(20)
            .WithMessage("The maximum length of Password is 20 characters.");

        RuleFor(m => m.UserName)
            .MaximumLength(30)
            .WithMessage("The maximum length of UserName is 30 characters.");
    }
}