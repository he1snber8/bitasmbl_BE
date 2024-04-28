namespace Project_Backend_2024.DTO;

public class User : IAuthenticatable, IDeletable 
{
    public int Id { get; }
    public bool IsDeleted { get; set; }
    public string? Username { get; set; } 
    public string? Password { get; set; } 
    public string? Email { get; set; } 
    public byte[]? Picture { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateJoined { get; set; }
    public DateTime? LastLogin { get; set; }

    public ICollection<Project>? Projects { get; set; }

}
