using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Facade.Models;

public class RegisterUserModel : IEntityModel, IAuthenticatable, IMailApplicable
{
    public int Id { get; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
