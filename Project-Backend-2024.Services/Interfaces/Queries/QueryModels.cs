
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services.Interfaces.Queries;

public interface IUserQueryService : IQueryModel<User> { }

public interface IProjectQueryService : IQueryModel<Project> { }

public interface IAppliedProjectQueryService : IQueryModel<AppliedProject> { }

public interface IUserSkillsQueryService : IQueryModel<UserSkills> { }

public interface ISkillQueryService : IQueryModel<Skill> { }
