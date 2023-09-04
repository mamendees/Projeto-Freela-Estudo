
namespace Freelancer.Application.ViewModels;
public class LoginUserViewModel
{
    public LoginUserViewModel(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public string Email { get; private set; } = string.Empty;
    public string Token { get; private set; } = string.Empty;
}
