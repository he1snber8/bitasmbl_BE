
using Project_Backend_2024.Services.Models;

namespace  Project_Backend_2024.Services.Interfaces.Commands;

public interface IUserCommand : ICommandModel<UserModel> { }

public interface IProjectCommand : ICommandModel<ProjectModel> { }

public interface IAppliedProjectCommand : ICommandModel<AppliedProjectModel> { }

public interface IUserSkillsCommand : ICommandModel<UserSkillsModel> { }

public interface ISkillCommand : ICommandModel<SkillModel> { }




