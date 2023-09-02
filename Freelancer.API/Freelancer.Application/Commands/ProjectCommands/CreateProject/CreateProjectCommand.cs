using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.CreateProject;
public class CreateProjectCommand : IRequest<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
}
