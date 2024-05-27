using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Facade.Models;

public class RegisterUserModel : IAuthenticatable, IMailApplicable
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
