using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Projects.Admin.List;

public class ListAllProjectsQueryAdminHandler(IMapper mapper, IProjectRepository projectRepository,IMemoryCache cache, CachingService cachingService)
    : QueryCachingService<List<Project>,List<ProjectModel>?>(cache,cachingService,mapper), IRequestHandler<ListAllProjectsQueryAdmin, List<ProjectModel>?>
{
    public async Task<List<ProjectModel>?> Handle(ListAllProjectsQueryAdmin request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.Set().Include(p => p.ProjectApplications)
            .ToListAsync(cancellationToken);

        var projectModels = InitializeQueryCaching("projects", projects);

        return projectModels;
    }
}