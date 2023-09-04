using Freelancer.Application.ViewModels;
using Freelancer.Core.Repositories;
using Freelancer.Core.Services;
using MediatR;

namespace Freelancer.Application.Commands.UserCommands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel?>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;
    public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<LoginUserViewModel?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Senha);
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
        if (user == null) return null;

        var token = _authService.GenerateJwtToken(user.Email, user.Role);
        return new LoginUserViewModel(user.Email, token);
    }
}
