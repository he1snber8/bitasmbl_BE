namespace Project_Backend_2024.Facade.Responses;

public record AuthenticatedUserResponse(string AccessToken, string RefreshToken);

public record AuthenticatedAdminResponse(string AccessToken, string RefreshToken);