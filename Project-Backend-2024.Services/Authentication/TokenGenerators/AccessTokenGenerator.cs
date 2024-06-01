using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AccessTokenGenerator : TokenGenerator
{
    private readonly AuthConfiguration _authConfiguration;

    public AccessTokenGenerator(AuthConfiguration authConfiguration)
    {
        _authConfiguration = authConfiguration;
    }

    private string GenerateAccessToken(User user)
         => GenerateToken(_authConfiguration.Key, _authConfiguration.Issuer,
            _authConfiguration.Audience, _authConfiguration.AccessTokenExpirationMinutes,GenerateClaims(user));

    public void SetAccessTokenInCookie(User user, HttpContext httpContext)
    {
        string newAccessToken = GenerateAccessToken(user);

        httpContext.Response.Cookies.Append("accessToken", newAccessToken, new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(_authConfiguration.AccessTokenExpirationMinutes),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }

    private List<Claim> GenerateClaims(User user)
    {
        return new List<Claim>
        {
            new Claim("Id", user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim (ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Role, "User")
        };
    }
}
