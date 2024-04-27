
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.Interfaces.Queries;

public interface IUserQuery : IQueryModel<UserModel> { }

public interface IProjectQuery : IQueryModel<ProjectModel> { }

public interface IAppliedProjectQuery : IQueryModel<AppliedProjectModel> { }

public interface IUserSkillsQuery : IQueryModel<UserSkillsModel> { }

public interface ISkillCommand : IQueryModel<SkillModel> { }
