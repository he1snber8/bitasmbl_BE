using MediatR;
using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Facade.AlterModels;

public record CreateProjectModel(
    string Name,
    string Description,
    string? GithubRepo,
    ushort CreationCost,
    List<ProjectRequirementModel> ProjectRequirements,
    // List<ProjectLink>? ProjectLinks,
    List<int> CategoryIds) : IRequest<int>;

public record ProjectRequirementModel(
    int RequirementId,
    RequirementModel? Requirement,
    byte MaxApplicationLimit,
    byte CurrentApplications,
    bool IsTestEnabled = false
    // int  RequirementTestId
) : IRequest<Unit>;

public class ProjectImageModel
{
    public required string ImageUrl { get; set; }
}

public class ProjectLinkModel
{
    public string UrlName { get; set; } = string.Empty; // Initialize to avoid nulls.
    public string UrlValue { get; set; } = string.Empty;
}

public record ApplyToProjectModel(
    string? CoverLetter,
    int ProjectId,
    List<int> RequirementIds,
    byte CorrectAnswers, // Number of correct answers
    byte TotalQuestions,
    List<string> SelectedAndAppliedRequirements) : IRequest<GetProjectApplicationModel>;
    
public record UploadProjectImagesModel(
    int ProjectId,
    List<IFormFile>? Files) : IRequest<Unit>;

public record DeleteProjectModel(int Id) : IRequest<Unit>;

public record UpdateProjectModel(int Id, string? Name, string? Description, string? Status)
    : IRequest<GetUserProjectModel>;