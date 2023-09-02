using Freelancer.Application.ViewModels;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Queries.UserQueries.GetUserById;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel?>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserViewModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null) return null;

        var userViewlModel = new UserViewModel(
            user.Id,
            user.FullName,
            user.Email,
            user.BirthDate
            );

        return userViewlModel;
    }
}
