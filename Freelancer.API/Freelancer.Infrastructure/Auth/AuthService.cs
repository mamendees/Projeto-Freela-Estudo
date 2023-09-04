using Freelancer.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Freelancer.Infrastructure.Auth;
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string email, string role)
    {
        string issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException();
        string audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException();
        string key = _configuration["Jwt:Key"] ?? throw new ArgumentNullException();

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("userName", email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer, 
            audience: audience, 
            expires: DateTime.Now.AddHours(4), 
            signingCredentials: credentials,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }

    public string ComputeSha256Hash(string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] bytes = SHA256.HashData(passwordBytes);
        return Convert.ToHexString(bytes);
    }
}
