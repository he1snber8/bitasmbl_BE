using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class User : IdentityUser, IEntity<string>, IDeletable
{
    public bool IsDeleted { get; set; }
    public byte[]? Picture { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateJoined { get; set; }
    public DateTime? LastLogin { get; set; }

    public ICollection<Project>? Projects { get; set; }
}
