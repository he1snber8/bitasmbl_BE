using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AdminRefreshTokenGenerator : TokenGenerator<AdminConfiguration>
{
    public AdminRefreshTokenGenerator(AdminConfiguration adminAuthConfiguration) : base(adminAuthConfiguration) { }

    public string GenerateRefreshToken() => GenerateToken(_configuration.RefreshKey, _configuration.Issuer,
            _configuration.Audience, _configuration.RefreshTokenExpirationMinutes);
}