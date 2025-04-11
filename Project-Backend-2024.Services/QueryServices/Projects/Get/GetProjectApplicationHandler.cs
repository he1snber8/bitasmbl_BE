using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public class GetProjectApplicationHandler(IProjectRepository repository, IMapper mapper)
    : IRequestHandler<GetProjectApplications, List<GetProjectApplicationModel>?>
{
    public async Task<List<GetProjectApplicationModel>?> Handle(GetProjectApplications request, CancellationToken cancellationToken)
    {
        var project = await repository.Set(p => p.Id == request.Id).Include(p => p.ProjectApplications).ThenInclude(p => p.Applicant).FirstOrDefaultAsync(cancellationToken);

        if (project is null) throw new ArgumentNullException(nameof(project), "Could not find project");

        var projectApplicationModels = mapper.Map<List<GetProjectApplicationModel>>(project.ProjectApplications);
        
        return projectApplicationModels;
    }
}