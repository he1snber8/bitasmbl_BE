using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.BasicGetModels;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

public class GetMyProjectApplicationsQueryHandler(IHttpContextAccessor httpContextAccessor,
    UserManager<User> userManager, IMapper mapper) : IRequestHandler<GetMyProjectApplicationsQuery, List<ProjectApplicationBasicGetModel>>
{
    public async Task<List<ProjectApplicationBasicGetModel>> Handle(GetMyProjectApplicationsQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return new List<ProjectApplicationBasicGetModel>();
        }

        var projectApplications = await userManager.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Projects.Where(p => p.PrincipalId == userId))
            .SelectMany(p => p.Applications.Where(a => a.PrincipalId == userId))
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ProjectApplicationBasicGetModel>>(projectApplications);
    }
}