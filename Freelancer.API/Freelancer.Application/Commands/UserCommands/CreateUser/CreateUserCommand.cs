using MediatR;

namespace Freelancer.Application.Commands.UserCommands.CreateUser;
public class CreateUserCommand : IRequest<int>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
