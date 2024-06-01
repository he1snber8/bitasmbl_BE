using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using System.Security.Claims;

namespace Project_Backend_2024.Services.TokenGenerators;

public class RefreshTokenGenerator : TokenGenerator
{
    private readonly AuthConfiguration _authConfiguration;

    public RefreshTokenGenerator(AuthConfiguration authConfiguration) { _authConfiguration = authConfiguration; }

    private string GenerateRefreshToken()
         => GenerateToken(_authConfiguration.RefreshKey,_authConfiguration.Issuer,
            _authConfiguration.Audience,_authConfiguration.RefreshTokenExpirationMinutes);

    public string SetRefreshTokenInCookie(HttpContext httpContext, string? storedRefreshToken = null)
    {
        string refreshToken = storedRefreshToken is null ? GenerateRefreshToken() : storedRefreshToken;

        httpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });

        return refreshToken;
    }

}
