using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class SkillCommandService : BaseCommandService<SkillModel, Skill, ISkillRepository>, ISkillCommandService
{
    public SkillCommandService(IUnitOfWork unitOfWork, IMapper mapper, ISkillRepository repository) : base(unitOfWork, mapper, repository) { }
}