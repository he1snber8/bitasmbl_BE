using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Projects.List;

public class ListAllProjectsQueryHandler(
    IMapper mapper,
    IProjectRepository projectRepository,
    IMemoryCache cache,
    IHttpContextAccessor httpContextAccessor,
    CachingService cachingService)
    : QueryCachingService<List<Project>, List<GetClientProjectModel?>>(cache, cachingService, mapper),
        IRequestHandler<ListAllProjectsQuery, List<GetClientProjectModel?>?>
{
    public async Task<List<GetClientProjectModel?>?> Handle(ListAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        IQueryable<Project> query = projectRepository.Set().Include(p => p.ProjectImages)
            .Include(p => p.User)
            .ThenInclude(u => u.UserSocials); // Include Requirement names via junction table

        if (userId is not null)
            query = query.Where(p => p.User.Id != userId);

        var projects = await query.ToListAsync(cancellationToken);

        var projectModels = InitializeQueryCaching(projects.Count.ToString(), projects);

        foreach (var project in projectModels)
        {
            // Fetch requirements and categories separately
            if (project != null)
            {
                project.Requirements = await FetchRequirementsAsync(project.Id, cancellationToken);
                project.Categories = await FetchCategoriesAsync(project.Id, cancellationToken);
                project.ProjectLinks = await FetchLinksAsync(project.Id, cancellationToken);
            }
        }

        return projectModels;
    }

    private async Task<List<ProjectRequirementModel?>> FetchRequirementsAsync(int projectId,
        CancellationToken cancellationToken)
    {
        try
        {
            var requirements = await projectRepository.Set()
                .Where(p => p.Id == projectId)
                .SelectMany(pr => pr.ProjectRequirements)
                .Where(pr => pr.ProjectId == projectId)
                .Include(pr => pr.Requirement) // Ensure the Requirement is included
                .ToListAsync(cancellationToken);


            var requirementModels = mapper.Map<List<ProjectRequirementModel?>>(requirements);

            return requirementModels;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<List<Category?>> FetchCategoriesAsync(int projectId, CancellationToken cancellationToken)
    {
        return await projectRepository.Set().Where(p => p.Id == projectId).SelectMany(pr => pr.ProjectCategories)
            .Where(pr => pr.ProjectId == projectId).Select(pr => pr.Category)
            .ToListAsync(cancellationToken);
    }
    
    private async Task<List<ProjectLinkModel?>> FetchLinksAsync(int projectId, CancellationToken cancellationToken)
    {
        var links = await projectRepository.Set().Where(p => p.Id == projectId)
            .SelectMany(pl => pl.ProjectLinks)
            .Where(pl => pl.ProjectId == projectId)
            .ToListAsync(cancellationToken);
        
        var linkModels = mapper.Map<List<ProjectLinkModel?>>(links);

        return linkModels;
    }
}