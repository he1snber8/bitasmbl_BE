
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Interfaces.Commands;

public interface IProjectCommandService : ICommandModel<ProjectModel>
{
    Task Update(string name, ProjectModel model,  string userId);
}

public interface IProjectApplicationCommandService : ICommandModel<ProjectApplicationModel> { }
public interface IUserSkillsCommandService : ICommandModel<UserSkillsModel> { }

public interface ISkillCommandService : ICommandModel<SkillModel> { }
