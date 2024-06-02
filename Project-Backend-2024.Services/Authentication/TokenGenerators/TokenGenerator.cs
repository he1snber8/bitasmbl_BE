using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Backend_2024.Services.TokenGenerators;

public abstract class TokenGenerator
{ 
    protected virtual string GenerateToken(string Key,string Issuer,string Audience,
        double TokenExpirationMinutes, IEnumerable<Claim>? claims = null)
    {
       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
       SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      
       JwtSecurityToken token = new(
           Issuer,
           Audience,
           claims,
           DateTime.UtcNow,
           DateTime.UtcNow.AddMinutes(TokenExpirationMinutes),
           credentials);
      
       return new JwtSecurityTokenHandler().WriteToken(token);
    }
}