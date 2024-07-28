using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

public class GetMyAppliedProjectsQueryHandler(IHttpContextAccessor httpContextAccessor,
    IMapper mapper, IProjectApplicationRepository applicationRepository, 
    IMemoryCache cache, CachingService cachingService) 
    : QueryCachingService<List<ProjectApplication>,List<GetAppliedProjectsModel?>>(cache,cachingService,mapper),
    IRequestHandler<GetMyAppliedProjectsQuery, List<GetAppliedProjectsModel?>?>
{
    
    public async Task<List<GetAppliedProjectsModel?>?> Handle(GetMyAppliedProjectsQuery request, CancellationToken cancellationToken)
    {
        var userId =  httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        if (string.IsNullOrEmpty(userId))
            return new List<GetAppliedProjectsModel?>();

        var projectApplications = await applicationRepository
            .Set(a => a.ApplicantId == userId).ToListAsync(cancellationToken);

        var appliedProjectsModels = InitializeQueryCaching($"key-{projectApplications.Count.ToString()}", projectApplications);

        return appliedProjectsModels;
    }
}