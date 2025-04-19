using MediatR;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Facade.AlterModels;

public record UserSkillsModel(GetSkillsModel Skills);

public record UserLoginModel(string? Email, string? Password, string? LoginType);

public record RegisterUserModel(
    string FirstName,
    string LastName,
    string? Email,
    string? ImageUrl,
    string? Password,
    string? RegistrationType);

public record UserUpdateModel(
    string? UserName,
    string? ImageUrl,
    string? Bio,
    string? Password,
    ushort Balance,
    List<UserSocialLinkModel?>? UserSocials) : IRequest<Unit>;

public record UserSocialLinkModel(string? SocialUrl) : IRequest<Unit>;
