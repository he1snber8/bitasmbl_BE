using Project_Backend_2024.Facade.Models;
using System.Security.Claims;

namespace Project_Backend_2024.Services.TokenGenerators;

public class RefreshTokenGenerator : TokenGenerator
{
    public RefreshTokenGenerator(AuthConfiguration authConfiguration) : base(authConfiguration) { }

    public string GenerateRefreshToken() => GenerateToken(_authConfiguration.RefreshKey, _authConfiguration.Issuer,
            _authConfiguration.Audience, _authConfiguration.RefreshTokenExpirationMinutes);
}