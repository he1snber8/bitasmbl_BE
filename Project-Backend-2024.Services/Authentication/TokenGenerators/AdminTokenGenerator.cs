using System.Security.Claims;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AdminTokenGenerator : TokenGenerator<AdminConfiguration>
{
    public AdminTokenGenerator(AdminConfiguration adminConfiguration) : base(adminConfiguration) { }

    public string GenerateToken(UserModel user)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim (ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        return GenerateToken(_configuration.Key, _configuration.Issuer,
            _configuration.Audience, _configuration.AccessTokenExpirationMinutes, claims);
    }
}