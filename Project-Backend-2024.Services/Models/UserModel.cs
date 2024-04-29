using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services.Models;

public class UserModel : IEntityModel, IAuthenticatable
{
    public int Id { get; }
    public string? Username { get; set; } 
    public string? Email { get; set; } 
    public string? Password { get; set; }
    public byte[]? Picture { get; set; }
    public string? Bio { get; set; }
    public DateTime? LastLogin { get; set; }
}
