using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class UserSkillsCommandService : BaseCommandService<UserSkillsModel, UserSkills, IUserSkillsRepository>, IUserSkillsCommandService
{
    public UserSkillsCommandService(IUnitOfWork unitOfWork, IMapper mapper, IUserSkillsRepository repository) : base(unitOfWork, mapper, repository) { }
}
