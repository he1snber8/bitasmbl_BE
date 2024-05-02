using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services.Models;

public class UserLoginModel : IAuthenticatable
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
