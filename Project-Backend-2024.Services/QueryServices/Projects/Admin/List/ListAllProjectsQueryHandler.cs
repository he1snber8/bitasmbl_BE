using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Projects.Admin.List;

public class ListAllProjectsQueryHandler(IMapper mapper, IProjectRepository projectRepository,IMemoryCache cache, CachingService cachingService)
    : QueryCachingService<List<Project>,List<ProjectModel>?>(cache,cachingService,mapper), IRequestHandler<ListAllProjectsQuery, List<ProjectModel>?>
{
    public async Task<List<ProjectModel>?> Handle(ListAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.Set().Include(p => p.Applications)
            .ToListAsync(cancellationToken);

        var projectModels = InitializeQueryCaching("", projects);

        return projectModels;
    }
}