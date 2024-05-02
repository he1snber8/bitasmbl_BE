﻿
using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AccessTokenGenerator
{

    private readonly IConfiguration _configuration;

    public AccessTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["Key"]));    
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim (ClaimTypes.Name, user.Username),
        };

        JwtSecurityToken token = new JwtSecurityToken(
            "http://localhost:5237", 
            "http://localhost:5237", 
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            credentials);
    }
}
