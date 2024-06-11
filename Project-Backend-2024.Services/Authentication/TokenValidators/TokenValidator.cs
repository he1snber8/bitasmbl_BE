using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Backend_2024.Services.TokenValidators;

public class TokenValidator
{
    public readonly AuthConfiguration _authConfiguration;

    public TokenValidator(AuthConfiguration authConfiguration)
    {
        _authConfiguration = authConfiguration;
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    { 
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.Key)),
            ValidIssuer = _authConfiguration.Issuer,
            ValidAudience = _authConfiguration.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    public bool IsRefreshTokenValid(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.RefreshKey)),
            ValidIssuer = _authConfiguration.Issuer,
            ValidAudience = _authConfiguration.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            tokenHandler.ValidateToken(refreshToken, validationParameters, out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
       
    }

}
