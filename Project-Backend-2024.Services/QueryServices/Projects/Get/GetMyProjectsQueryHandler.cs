using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.BasicGetModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public class GetMyProjectsQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IMapper mapper) : IRequestHandler<GetMyProjectsQuery,List<ProjectBasicGetModel?>?>
{
    public async Task<List<ProjectBasicGetModel?>?> Handle(GetMyProjectsQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        var projects = await userManager.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Projects.Where(p => p.PrincipalId == userId)).ToListAsync(cancellationToken);
        
        var projectModels = mapper.Map<List<ProjectBasicGetModel?>>(projects);

        return projectModels;
    }
}