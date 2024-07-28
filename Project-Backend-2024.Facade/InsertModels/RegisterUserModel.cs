using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Facade.InsertModels;

public record RegisterUserModel()
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
