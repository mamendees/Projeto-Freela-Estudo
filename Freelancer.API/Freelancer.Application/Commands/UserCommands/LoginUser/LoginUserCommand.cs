using Freelancer.Application.ViewModels;
using MediatR;

namespace Freelancer.Application.Commands.UserCommands.LoginUser;
public class LoginUserCommand : IRequest<LoginUserViewModel?>
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
