// using AutoMapper;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Caching.Memory;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade.FetchModels;
// using Project_Backend_2024.Facade.GetModels;
// using Project_Backend_2024.Facade.Interfaces;
// using Project_Backend_2024.Services.Caching;
//
// namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;
//
// public class GetMyAppliedProjectsQueryHandler(
//     IHttpContextAccessor httpContextAccessor,
//     IMapper mapper,
//     IProjectRepository projectRepository,
//     IMemoryCache cache,
//     UserManager<User> userManager,
//     CachingService cachingService)
//     : QueryCachingService<List<ProjectApplication>, List<GetUserAppliedProjectModel?>>(cache, cachingService, mapper),
//         IRequestHandler<GetMyAppliedProjectsQuery, List<GetUserAppliedProjectModel?>?>
// {
//     public async Task<List<GetUserAppliedProjectModel?>?> Handle(GetMyAppliedProjectsQuery request,
//         CancellationToken cancellationToken)
//     {
//         var userId = httpContextAccessor.HttpContext?.User?.Claims
//             .FirstOrDefault(c => c.Type == "Id")?.Value;
//
//        //
//        //  var appliedProjects = await userManager.FindByIdAsync(userId);
//        //
//        // var lol =  appliedProjects.AppliedProjects;
//
//         if (string.IsNullOrEmpty(userId))
//             return new List<GetUserAppliedProjectModel?>();
//         
//         var projectApplications = await projectRepository
//             .Set().SelectMany(pr => pr.ProjectApplications).Include(pa => pa.Project)
//             .Where(p => p.ApplicantId == userId)
//             .Include(p => p.Applicant)
//             .ToListAsync(cancellationToken);
//         
//        var appliedProjectsModels = InitializeQueryCaching(projectApplications.Count.ToString(), projectApplications);
//        
//         return appliedProjectsModels;
//     }
// }