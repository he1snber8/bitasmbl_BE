using Project_Backend_2024.Facade.Models;
using System.Security.Claims;

namespace Project_Backend_2024.Services.TokenGenerators;

public class UserRefreshTokenGenerator : TokenGenerator<UserConfiguration>
{
    public UserRefreshTokenGenerator(UserConfiguration userAuthConfiguration) : base(userAuthConfiguration) { }

    public string GenerateRefreshToken() => GenerateToken(_configuration.RefreshKey, _configuration.Issuer,
            _configuration.Audience, _configuration.RefreshTokenExpirationMinutes);
}
