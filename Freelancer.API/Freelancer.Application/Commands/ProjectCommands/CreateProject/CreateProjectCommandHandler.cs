using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.CreateProject;
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IProjectRepository _projectRepository;
    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);
        await _projectRepository.AddAsync(project);
        return project.Id;
    }
}
