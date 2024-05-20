using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.Facade.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Project_Backend_2024.Services.TokenValidators;

public class RefreshTokenValidator
{
    public readonly UserConfiguration _authConfiguration;

    public RefreshTokenValidator(UserConfiguration authConfiguration)
    {
        _authConfiguration = authConfiguration;
    }

    public bool Validate(string token)
    {
        try
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

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

}
