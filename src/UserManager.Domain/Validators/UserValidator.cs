using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using UserManager.Domain.Entities;

namespace UserManager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Entity cannot be empty")

            .NotNull()
            .WithMessage("Entity cannot be null");

            RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Name cannot be null")

            .NotEmpty()
            .WithMessage("Name cannot be empty")

            .MinimumLength(3)
            .WithMessage("Name must have at least 3 characters")

            .MaximumLength(80)
            .WithMessage("Name must have at most 80 characters");

            RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Email cannot be null")

            .NotEmpty()
            .WithMessage("Email cannot be empty")

            .MinimumLength(10)
            .WithMessage("Email must have at least 10 characters")

            .MaximumLength(180)
            .WithMessage("Email must have at most 180 characters")

            .EmailAddress()
            .WithMessage("Email must be a valid email address");

        }
    }
}