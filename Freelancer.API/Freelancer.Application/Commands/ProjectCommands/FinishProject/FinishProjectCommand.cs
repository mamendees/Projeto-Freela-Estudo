using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.FinishProject;
public class FinishProjectCommand: IRequest<Unit>
{
    public int Id { get; set; }
    public string CreditCardNumber { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public string ExpiresAt { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
