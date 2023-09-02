using Freelancer.Application.ViewModels;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Queries.ProjectQueries.GetProjectById;
public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel?>
{
    private readonly IProjectRepository _projectRepository;
    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDetailsViewModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project is null) return null;
        
        var projectDetailViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt,
            project.Client.FullName,
            project.Freelancer.FullName
            );

        return projectDetailViewModel;
    }
}
