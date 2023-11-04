﻿using FluentValidation;
using MembersService.Api.Dtos;

namespace MembersService.Api.Validators;

public class MemberValidator : AbstractValidator<MemberDto>
{
    public MemberValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty()
            .WithMessage("The FirstName is required.")
            .MaximumLength(50)
            .WithMessage("The maximum length of FirstName is 50 characters.");

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