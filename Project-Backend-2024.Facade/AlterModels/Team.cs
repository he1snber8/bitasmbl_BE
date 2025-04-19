using MediatR;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.AlterModels;

public record InitializeTeamCommand(
    string Name,
    string? ContactName,
    string? ContactEmail,
    string? Location,
    int YearsInOperation,
    string? CompanyProfileUrl,
    string? LogoUrl
) : IRequest<Unit>;

public record PopulateTeamWithMembersCommand(
    int TeamId,
    List<TeamMemberModel> Members
) : IRequest<Unit>;
