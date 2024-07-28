using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public class GetProjectByNameQueryHandler(IProjectRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectByNameQuery, GetProjectModel?>
{
    public async Task<GetProjectModel?> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
    {
        var project = await repository.Set(p => p.Name == request.Name).FirstOrDefaultAsync(cancellationToken);

        if (project is null) throw new ArgumentNullException(nameof(project), "Could not find project");

        var model = mapper.Map<GetProjectModel>(project);
        
        return model;
    }
}