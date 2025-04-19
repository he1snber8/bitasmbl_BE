using Amazon.Util.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

public class User : IdentityUser, IEntity<string>
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public RegistrationType? RegistrationType { get; set; }
    public string? ResumeUrl { get; set; }
    public bool IsDeleted { get; set; }
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }
}

public class TeamManager : User
{
    public int? TeamId { get; set; }
    public required string Role { get; set; } // e.g. Frontend Developer, DevOps Engineer
    public string? SocialPortfolioUrl { get; set; }

    public Team? Team { get; set; }
    public List<UserSocialLink?>? UserSocials { get; set; } = new();
    public List<Requirement> UserRequirementSkills { get; set; } = new();
}

public class OrganizationManager : User
{
    public int? OrganizationId { get; set; }
    public bool IsVerified { get; set; }
    public string? Department { get; set; }

    public Organization? Organization { get; set; }
}

