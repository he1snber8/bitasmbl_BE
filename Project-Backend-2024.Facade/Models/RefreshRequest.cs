namespace Project_Backend_2024.Facade.Models;

public record RefreshRequest(string accessToken = null!,string refreshToken = null!);

public record AccessToken(string accessToken = null!);
