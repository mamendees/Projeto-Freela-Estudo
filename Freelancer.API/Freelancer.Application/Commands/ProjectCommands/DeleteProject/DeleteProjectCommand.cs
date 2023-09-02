using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.DeleteProject;
public class DeleteProjectCommand : IRequest<Unit>
{
    public DeleteProjectCommand(int id)
    {
        Id = id;
    }

    public int Id  { get; private set; }
}
