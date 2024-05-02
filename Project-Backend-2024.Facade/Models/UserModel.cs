using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.Models;

public class UserModel : IEntityModel, IAuthenticatable, IMailApplicable
{
    public int Id { get; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public byte[]? Picture { get; set; }
    public string? Bio { get; set; }
    public DateTime? LastLogin { get; set; }
}
