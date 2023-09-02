using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.UpdateProject;
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project == null) return Unit.Value;

        project.Update(request.Title, request.Description, request.TotalCost);
        await _projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
