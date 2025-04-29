using UserManagement.App.Dtos;
using FluentValidation;
namespace UserManagement.App.Validations;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(u => u.Fullname)
            .NotEmpty()
            .WithMessage("Fullname is required.")
            .MaximumLength(100)
            .WithMessage("Fullname must be less than 100 characters.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MaximumLength(50)
            .WithMessage("Username must be less than 50 characters.");

        
    }
}