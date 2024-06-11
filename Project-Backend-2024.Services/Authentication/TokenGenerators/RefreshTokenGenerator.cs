using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using System.Security.Claims;

namespace Project_Backend_2024.Services.TokenGenerators;

public class RefreshTokenGenerator : TokenGenerator
{
    private readonly AuthConfiguration _authConfiguration;

    public RefreshTokenGenerator(AuthConfiguration authConfiguration) { _authConfiguration = authConfiguration; }

    public string GenerateRefreshToken()
         => GenerateToken(_authConfiguration.RefreshKey,_authConfiguration.Issuer,
            _authConfiguration.Audience,_authConfiguration.RefreshTokenExpirationMinutes);
}
