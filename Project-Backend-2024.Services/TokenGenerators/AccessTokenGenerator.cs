using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Project_Backend_2024.Services.TokenGenerators.Interfaces;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AccessTokenGenerator : IUserAccessToken
{
    private readonly AuthConfiguration _authConfiguration;

    public AccessTokenGenerator(AuthConfiguration authConfiguration)
    {
        _authConfiguration = authConfiguration;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.Key));    
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim (ClaimTypes.Name, user.Username),
            
        };

        JwtSecurityToken token = new JwtSecurityToken(
            _authConfiguration.Issuer,
            _authConfiguration.Audience, 
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_authConfiguration.AccessTokenExpirationMinutes),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


