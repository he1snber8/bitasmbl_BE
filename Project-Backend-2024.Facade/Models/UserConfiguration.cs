namespace Project_Backend_2024.Facade.Models;

public abstract class AuthConfiguration
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public double AccessTokenExpirationMinutes { get; set; }

    public string RefreshKey { get; set; } = null!;
    public double RefreshTokenExpirationMinutes { get; set; }
}

public class UserConfiguration : AuthConfiguration { }

public class AdminConfiguration :  AuthConfiguration { }