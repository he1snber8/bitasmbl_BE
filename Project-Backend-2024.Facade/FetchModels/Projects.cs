using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Facade.FetchModels;

public record GetUserProjectModel(
    int Id,
    string? Name,
    string? Description,
    string? Status,
    string? GithubRepo,
    DateTime? DateCreated,
    byte Applications,
    
    ICollection<ProjectImageModel>? ProjectImages,
    ICollection<GetProjectApplicationModel>? ProjectApplications);
    
    
public class GetUserAppliedProjectModel()
{
    public string? ApplicationStatus { get; set; }
    public DateTime? DateApplied { get; set; }
    public required GetClientProjectModel Project { get; set; }
    public string? CoverLetter { get; set; }
}

public record GetProjectApplicationModel(
    int Id,
    string? CoverLetter,
    string? ApplicationStatus,
    // int ProjectId,
    // int TeamId,
    GetClientProjectModel GetClientProjectModel,
    List<string> SelectedAndAppliedRequirements,
    GetTeamModel? Team);

// public string? CoverLetter { get; set; }
//
// public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
//
// public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
//     
// public Team? Team { get; set; }
// public Project Project { get; set; }
//     
    
public class GetClientProjectModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? GithubRepo { get; set; }
    public byte Applications { get; set; }
    public DateTime? DateCreated { get; set; }
    public required GetUserProfileModel User { get; set; }
    public List<ProjectRequirementModel?> Requirements { get; set; } = new();

    public List<ProjectLinkModel?> ProjectLinks { get; set; } = new();

    public List<ProjectImageModel?> ProjectImages { get; set; } = new();
    public List<Category?> Categories { get; set; } = new();
}

public record RequirementModel(string Name);

public class GetCategoriesModel : IRequest<List<Category>> {}

public class GetRequirementsModel : IRequest<List<Requirement>> {}