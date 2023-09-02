using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.StartProject;
public class StartProjectCommand: IRequest<Unit>
{
    public StartProjectCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
