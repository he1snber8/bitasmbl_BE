using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public class GetProjectByIdQueryHandler(
    IProjectRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetProjectByIdQuery, GetUserProjectModel?>
{
    public async Task<GetUserProjectModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        if (userId is null) throw new UnauthorizedAccessException();

        var project = await repository.Set(p => p.Id == request.Id)
            .Include(p => p.ProjectApplications)    
            .ThenInclude(pa => pa.Applicant)
            .Where(p => p.PrincipalId == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null) return null;

        var model = mapper.Map<GetUserProjectModel>(project);

        return model;
    }
}