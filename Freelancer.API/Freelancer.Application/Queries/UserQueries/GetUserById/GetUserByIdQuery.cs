using Freelancer.Application.ViewModels;
using MediatR;

namespace Freelancer.Application.Queries.UserQueries.GetUserById;
public class GetUserByIdQuery : IRequest<UserViewModel?>
{
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
