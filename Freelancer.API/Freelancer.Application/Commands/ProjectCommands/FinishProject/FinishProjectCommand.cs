using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.FinishProject;
public class FinishProjectCommand: IRequest<Unit>
{
    public FinishProjectCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
