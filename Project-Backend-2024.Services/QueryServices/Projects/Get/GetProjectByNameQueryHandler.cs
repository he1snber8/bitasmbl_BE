using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.BasicGetModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public class GetProjectByNameQueryHandler(IProjectRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectByNameQuery, ProjectBasicGetModel?>
{
    public async Task<ProjectBasicGetModel?> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
    {
        var project = await repository.Set(p => p.Name == request.Name).FirstOrDefaultAsync(cancellationToken);

        if (project is null) throw new ArgumentNullException(nameof(project), "Could not find project");

        var model = mapper.Map<ProjectBasicGetModel>(project);
        
        return model;
    }
}