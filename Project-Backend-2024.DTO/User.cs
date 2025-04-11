using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class User : IdentityUser, IEntity<string>
{
    public bool IsDeleted { get; set; }
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }

    public ushort Balance { get; set; }
    public DateTime? DateJoined { get; set; }
    public DateTime? LastLogin { get; set; }
    public RegistrationType? RegistrationType { get; set; }

    // Navigation Properties
    public List<Project> Projects { get; set; } = new();
    public List<ProjectApplication> ProjectApplications { get; set; } = new();
    
    public List<UserSocialLink?>? UserSocials { get; set; } = new();
    public List<UserAppliedProject> AppliedProjects { get; set; } = new();
    public List<UserSkills> UserSkills { get; set; } = new();
    
    public List<Transaction> Transactions { get; set; } = new();
}