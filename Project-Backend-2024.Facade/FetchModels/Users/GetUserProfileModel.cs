using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AlterModels;

namespace Project_Backend_2024.Facade.FetchModels;

public class GetUserProfileModel : IRequest<GetUserProfileModel>
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }
    public ushort Balance { get; set; }
    public DateTime? DateJoined { get; set; }
    // public DateTime? LastLogin { get; set; }
    public List<GetUserProjectModel> Projects { get; set; } = new();
    public List<GetSkillsModel> Skills { get; set; } = new();
    public List<UserSocialLinkModel> UserSocials { get; set; } = new();
}
