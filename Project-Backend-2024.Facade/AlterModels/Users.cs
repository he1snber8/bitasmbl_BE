using MediatR;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Facade.AlterModels;

public record UserSkillsModel(GetSkillsModel Skills);

public record UserLoginModel(string? Email, string? Username, string? Password, string? LoginType = "Standard");

public record RegisterUserModel(
    string Username,
    string? Email,
    string? ImageUrl,
    string? Password,
    string? RegistrationType = "Standard");

public record UserUpdateModel(
    string? UserName,
    string? ImageUrl,
    string? Bio,
    string? Password,
    ushort Balance,
    List<UserSocialLinkModel?>? UserSocials) : IRequest<Unit>;

public record UserSocialLinkModel(string? SocialUrl) : IRequest<Unit>;
