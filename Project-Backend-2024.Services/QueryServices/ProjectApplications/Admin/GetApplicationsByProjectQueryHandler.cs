using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Admin;

public class GetApplicationsByProjectQueryHandler(IMapper mapper, IMemoryCache cache,CachingService cachingService)
    : QueryCachingService<List<ProjectApplicationModel>, List<GetProjectApplicationModel>>
        (cache,cachingService,mapper),IRequestHandler<GetApplicationsByProjectQuery, List<ProjectApplicationModel>>
{
    public Task<List<ProjectApplicationModel>> Handle(GetApplicationsByProjectQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}