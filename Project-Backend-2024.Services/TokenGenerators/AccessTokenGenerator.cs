using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AccessTokenGenerator : TokenGenerator
{
    public AccessTokenGenerator(AuthConfiguration authConfiguration) : base(authConfiguration) { }

    public string GenerateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim (ClaimTypes.Name, user.Username),
        };

        return  GenerateToken(_authConfiguration.Key, _authConfiguration.Issuer,
            _authConfiguration.Audience, _authConfiguration.AccessTokenExpirationMinutes,claims);
    }
}
