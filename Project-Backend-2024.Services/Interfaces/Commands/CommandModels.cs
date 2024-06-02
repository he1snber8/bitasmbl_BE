
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Interfaces.Commands;

public interface IProjectCommandService : ICommandModel<ProjectModel> { }

public interface IAppliedProjectCommandService : ICommandModel<AppliedProjectModel> { }

public interface IUserSkillsCommandService : ICommandModel<UserSkillsModel> { }

public interface ISkillCommandService : ICommandModel<SkillModel> { }
