using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Projects;

public class CreateProjectCommand(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IProjectRepository projectRepository,
    ILogger<CreateProjectCommand> logger, UserManager<User> userManager)
    : IRequestHandler<CreateProjectModel, int>
{
    public async Task<int> Handle(CreateProjectModel request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);

        var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                     ?? throw new UserNotFoundException();

        var user = await userManager.FindByIdAsync(userId);

        await EnsureProjectDoesNotExistAsync(request.Name, cancellationToken);

        var project = mapper.Map<Project>(request);

        // if (user.Balance - request.CreationCost < 0)
        // {
        //     throw new InsufficientFundsException();
        // }
        
        project.PrincipalId = userId;
        // user.Balance -= request.CreationCost;
        
        LinkProjectRequirements(request.ProjectRequirements, project);
        LinkProjectCategories(request.CategoryIds, project);
        // LinkProjectUsefulLinks(request.ProjectLinks, project);
        
        projectRepository.Insert(project);

        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("[{Date}] User with an ID {userId} created project successfully.",
            DateTime.Now, userId);

        return project.Id;
    }

    private void LinkProjectRequirements(List<ProjectRequirementModel> requirements, Project project)
    {
        if (requirements.Count <= 0)
            throw new ProjectRequirementNotSelectedException();

        project.ProjectRequirements = requirements
            .Select(requirement => new ProjectRequirement
            {
                ProjectId = project.Id,
                RequirementId = requirement.RequirementId,
                MaxApplicationLimit = requirement.MaxApplicationLimit,
                IsTestEnabled = requirement.IsTestEnabled
            }).ToList();
    }


    private void LinkProjectCategories(List<int> categoryIds, Project project)
    {
        if (categoryIds.Count <= 0)
            throw new ProjectCategoryNotSelectedException();

        project.ProjectCategories = categoryIds
            .Select(id => new ProjectCategory
            {
                ProjectId = project.Id,
                CategoryId = id
            })
            .ToList();
    }

    // private void LinkProjectUsefulLinks(List<ProjectLink>? projectLinks, Project project)
    // {
    //     
    //     project.ProjectLinks = projectLinks
    //         .Select(pk => new ProjectLink
    //         {
    //             ProjectId = project.Id,
    //             UrlName = pk.UrlName,
    //             UrlValue = pk.UrlValue
    //         })
    //         .ToList();
    // }

    private void ValidateRequest(CreateProjectModel request)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new EmptyValueException(nameof(request.Name));

        if (string.IsNullOrEmpty(request.Description))
            throw new EmptyValueException(nameof(request.Description));
    }

    private async Task EnsureProjectDoesNotExistAsync(string projectName, CancellationToken cancellationToken)
    {
        var existingProject = await projectRepository.Set(p => p.Name == projectName)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProject is not null)
            throw new EntityAlreadyExistsException("Project with name ", existingProject.Name,
                ", please choose something different");
    }
}