using Freelancer.Application.ViewModels;
using MediatR;

namespace Freelancer.Application.Queries.ProjectQueries.GetProjectById;
public class GetProjectByIdQuery : IRequest<ProjectDetailsViewModel?>
{
    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
