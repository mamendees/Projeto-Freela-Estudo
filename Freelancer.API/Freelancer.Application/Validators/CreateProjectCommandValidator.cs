

using FluentValidation;
using Freelancer.Application.Commands.ProjectCommands.CreateProject;

namespace Freelancer.Application.Validators;
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.Description).MaximumLength(255);
        RuleFor(p => p.Title).MaximumLength(30);
    }
}
