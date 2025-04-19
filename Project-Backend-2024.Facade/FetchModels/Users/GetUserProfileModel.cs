using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.AlterModels;

namespace Project_Backend_2024.Facade.FetchModels;

public class GetUserProfileModel : IEntity<string>
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }
    public string? RegistrationType { get; set; }
    // public string? ResumeUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DateJoined { get; set; }
    public ushort Balance { get; set; } // Where's this coming from, btw?
}

public class GetTeamManagerProfileModel : GetUserProfileModel
{
    public int? TeamId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string? SocialPortfolioUrl { get; set; }

    public GetTeamModel? Team { get; set; }
    // public List<UserSocialLinkModel> UserSocials { get; set; } = new();
    // public List<GetSkillModel> Skills { get; set; } = new(); // mapped from Requirements
}

// public class GetOrganizationManagerProfileModel : GetUserProfileModel
// {
//     public int? OrganizationId { get; set; }
//     public bool IsVerified { get; set; }
//     public string? Department { get; set; }
//
//     public Organization? Organization { get; set; }
// }



