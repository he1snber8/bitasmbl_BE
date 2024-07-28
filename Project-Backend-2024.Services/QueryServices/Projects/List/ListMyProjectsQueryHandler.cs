using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Projects.List;

public class ListMyProjectsQueryHandler(IHttpContextAccessor httpContextAccessor, 
    UserManager<User> userManager,IMemoryCache cache, CachingService cachingService, IMapper mapper) 
    : QueryCachingService<List<Project?>,List<GetProjectModel?>?>(cache,cachingService,mapper),IRequestHandler<ListMyProjectsQuery,List<GetProjectModel?>?>
{
    public async Task<List<GetProjectModel?>?> Handle(ListMyProjectsQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        var projects = await userManager.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Projects.Where(p => p.PrincipalId == userId)).ToListAsync(cancellationToken);
        
        var projectModels = InitializeQueryCaching(projects.Count.ToString(), projects);

        return projectModels;
    }
}