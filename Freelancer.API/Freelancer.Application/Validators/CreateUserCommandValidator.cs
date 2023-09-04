using FluentValidation;
using Freelancer.Application.Commands.UserCommands.CreateUser;

namespace Freelancer.Application.Validators;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(p => p.FullName).NotNull().NotEmpty();
        RuleFor(p => p.BirthDate).NotNull().NotEmpty();
    }
}
