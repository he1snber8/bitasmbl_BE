using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.FetchModels;

public record GetTeamModel(
    int Id,
    string Name,
    string? ContactName,
    string ContactEmail,
    string? Location,
    int YearsInOperation,
    string? CompanyProfileUrl,
    string? LogoUrl,
    List<TeamMemberModel> Members
);
