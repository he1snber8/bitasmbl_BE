
using Project_Backend_2024.Services.Models;

namespace  Project_Backend_2024.Services.Interfaces.Commands;

public interface IUserCommandService : ICommandModel<UserModel> { }

public interface IProjectCommandService : ICommandModel<ProjectModel> { }

public interface IAppliedProjectCommandService : ICommandModel<AppliedProjectModel> { }

public interface IUserSkillsCommandService : ICommandModel<UserSkillsModel> { }

public interface ISkillCommandService : ICommandModel<SkillModel> { }




