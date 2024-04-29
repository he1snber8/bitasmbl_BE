using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class ProjectCommandService : BaseCommandService<ProjectModel, Project, IProjectRepository>, IProjectCommandService
{
    public ProjectCommandService(IUnitOfWork unitOfWork, IMapper mapper, IProjectRepository repository) : base(unitOfWork, mapper, repository) { }
}
