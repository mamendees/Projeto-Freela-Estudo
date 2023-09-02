using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.DeleteProject;
public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project is null) return Unit.Value;

        if (project.SetAndReturnIfCanCancel()) await _projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
