﻿using Freelancer.Application.ViewModels;
using Freelancer.Core.Models;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Queries.ProjectQueries.GetAllProjects;
public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
{
    private readonly IProjectRepository _projectRepository;
    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var paginationProjects = await _projectRepository.GetAllAsync(request.Query, request.Page, request.PageSize);
        var projectsViewModel = paginationProjects.Data.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();

        var paginationProectsViewModel = new PaginationResult<ProjectViewModel>(
            paginationProjects.Page,
            paginationProjects.TotalPages,
            paginationProjects.PageSize,
            paginationProjects.ItemsCount,
            projectsViewModel
            );

        return paginationProectsViewModel;
    }
}
