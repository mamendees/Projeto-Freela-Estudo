using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.StartProject;
public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    public StartProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project is null) return Unit.Value;

        if (project.SetAndReturnIfCanStart()) await _projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
