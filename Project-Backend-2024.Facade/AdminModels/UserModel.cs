using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Facade.AdminModels;

public class UserModel
{
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? RegistrationType { get; set; }
    public string? Bio { get; set; }
}
