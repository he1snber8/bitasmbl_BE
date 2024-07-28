using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

public class GetMyProjectApplicationsQueryHandler
(IHttpContextAccessor httpContextAccessor,IMapper mapper,IMemoryCache cache, CachingService cachingService,
    UserManager<User> userManager) 
    : QueryCachingService<List<ProjectApplication>,List<GetProjectApplicationModel?>>(cache,cachingService,mapper),
        IRequestHandler<GetMyProjectApplicationsQuery, List<GetProjectApplicationModel?>?>
{
    public async Task<List<GetProjectApplicationModel?>?> Handle(GetMyProjectApplicationsQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        if (string.IsNullOrEmpty(userId))
            return new List<GetProjectApplicationModel?>();

        var projects =  userManager.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Projects.Where(p => p.PrincipalId == userId));

        if (request.Name is not null)
            projects = projects.Where(p => p.Name == request.Name);

        var projectApplications = await projects
            .SelectMany(p => p.Applications.Where(a => a.PrincipalId == userId)).ToListAsync(cancellationToken);
           
        var projectApplicationModels = InitializeQueryCaching($"key-{projectApplications.Count.ToString()}", projectApplications);

        return projectApplicationModels;
    }
}