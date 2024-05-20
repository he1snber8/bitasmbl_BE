using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class User : IEntity, IAuthenticatable, IDeletable, IMailApplicable
{
    public int Id { get; }
    public bool IsDeleted { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[]? Picture { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateJoined { get; set; }
    public DateTime? LastLogin { get; set; }

    public ICollection<Project>? Projects { get; set; }
    public RefreshToken? RefreshToken { get; set; }

}
