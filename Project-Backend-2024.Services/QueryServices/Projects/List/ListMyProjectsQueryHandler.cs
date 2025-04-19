// using AutoMapper;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Caching.Memory;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Facade.FetchModels;
// using Project_Backend_2024.Facade.GetModels;
// using Project_Backend_2024.Services.Caching;
//
// namespace Project_Backend_2024.Services.QueryServices.Projects.List;
//
// public class ListMyProjectsQueryHandler(
//     IHttpContextAccessor httpContextAccessor,
//     UserManager<User> userManager,
//     IMemoryCache cache,
//     CachingService cachingService,
//     IMapper mapper)
//     : QueryCachingService<List<Project?>, List<GetUserProjectModel?>?>(cache, cachingService, mapper),
//         IRequestHandler<ListMyProjectsQuery, List<GetUserProjectModel?>?>
// {
//     public async Task<List<GetUserProjectModel?>?> Handle(ListMyProjectsQuery request,
//         CancellationToken cancellationToken)
//     {
//         var userId = httpContextAccessor.HttpContext.User.Claims
//             .FirstOrDefault(c => c.Type == "Id")?.Value ?? throw new UserNotFoundException();
//
//         var user = await userManager.Users
//             .Where(u => u.Id == userId)
//             .Include(p => p.Projects)
//             .ThenInclude(pi => pi.ProjectCategories)
//             .Include(p => p.Projects)// Ensure Projects are included
//             .ThenInclude(pa => pa.ProjectRequirements)
//             .Include(p => p.Projects)
//             .ThenInclude(pa => pa.ProjectApplications)
//             .ThenInclude(a => a.Applicant)
//             // Include Applications for Projects
//             .FirstOrDefaultAsync(cancellationToken) ?? throw new UserNotFoundException();
//
//         var projectModels = InitializeQueryCaching(user.Projects.Count.ToString(), user.Projects);
//
//         return string.IsNullOrWhiteSpace(request.ProjectName)
//             ? projectModels
//             : projectModels.Where(p => p.Name == request.ProjectName).ToList();
//     }
// }