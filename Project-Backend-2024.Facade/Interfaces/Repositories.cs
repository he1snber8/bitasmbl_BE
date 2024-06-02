using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.Interfaces;

public interface ISkillRepository : IRepositoryBase<Skill> { }

public interface IUserSkillsRepository : IRepositoryBase<UserSkills> { }

public interface IProjectRepository : IRepositoryBase<Project> { }

public interface IAppliedProjectRepository : IRepositoryBase<AppliedProject> { }
  