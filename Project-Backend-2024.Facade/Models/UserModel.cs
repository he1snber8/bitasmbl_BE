using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Facade.Models;

public class UserModel : IEntityModel, IAuthenticatable, IMailApplicable
{
    public int Id { get; set; }
    public string? Username { get; set; } 
    public string? Email { get; set; } 
    public string? Password { get; set; } 
    public byte[]? Picture { get; set; }
    public string? Bio { get; set; }
    public DateTime? LastLogin { get; set; }
}
