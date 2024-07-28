using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Projects.List;

public class ListAllProjectsQueryHandler(IMapper mapper, IProjectRepository projectRepository,IMemoryCache cache, CachingService cachingService)
    : QueryCachingService<List<Project>,List<GetProjectModel?>>(cache,cachingService,mapper),
        IRequestHandler<ListAllProjectsQuery, List<GetProjectModel?>?>
{
    public async Task<List<GetProjectModel?>?> Handle(ListAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAll();

        var projectModels = InitializeQueryCaching(projects.Count.ToString(), projects);

        return projectModels;
    }
}