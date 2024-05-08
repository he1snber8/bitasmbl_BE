namespace Project_Backend_2024.Facade.Responses;

public class AuthenticatedUserResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
