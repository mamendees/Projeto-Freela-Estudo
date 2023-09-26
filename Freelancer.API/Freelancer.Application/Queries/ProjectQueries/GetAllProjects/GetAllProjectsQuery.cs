using Freelancer.Application.ViewModels;
using Freelancer.Core.Models;
using MediatR;

namespace Freelancer.Application.Queries.ProjectQueries.GetAllProjects;
public class GetAllProjectsQuery : IRequest<PaginationResult<ProjectViewModel>>
{
    
    public string? Query { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
