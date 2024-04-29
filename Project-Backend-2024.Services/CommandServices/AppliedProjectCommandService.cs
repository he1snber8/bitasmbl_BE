using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class AppliedProjectCommandService : BaseCommandService<AppliedProjectModel, AppliedProject, IAppliedProjectRepository>, IAppliedProjectCommandService
{
    public AppliedProjectCommandService(IUnitOfWork unitOfWork, IMapper mapper, IAppliedProjectRepository repository) : base(unitOfWork, mapper, repository) { }
}
