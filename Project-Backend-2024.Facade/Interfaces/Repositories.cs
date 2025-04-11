using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.Interfaces;

public interface ISkillRepository : IRepositoryBase<Skill> { }
public interface IUserRepository : IRepositoryBase<User> { }
public interface IUserSkillsRepository : IRepositoryBase<UserSkills> { }

public interface IProjectRepository : IRepositoryBase<Project> { }

public interface ITransactionRepository : IRepositoryBase<Transaction> { }

public interface ICategoryRepository : IRepositoryBase<Category> { }

public interface IRequirementRepository : IRepositoryBase<Requirement> { }
public interface IProjectImagesRepostiory : IRepositoryBase<ProjectImage> { }
  