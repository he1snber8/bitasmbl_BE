using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Facade.Interfaces;

// public interface ISkillRepository : IRepositoryBase<Skill> { }
public interface IUserRepository<TUser> : IRepositoryBase<TUser>
    where TUser : User {}
public interface ITeamManagerRepository : IUserRepository<TeamManager> { }
public interface IOrganizationManagerRepository : IUserRepository<OrganizationManager> { }
public interface IOrganizationRepository : IRepositoryBase<Organization> { }
public interface ITeamRepository : IRepositoryBase<Team> { }

public interface IProjectRepository : IRepositoryBase<Project> { }

public interface ITransactionRepository : IRepositoryBase<Transaction> { }

public interface ICategoryRepository : IRepositoryBase<Category> { }

public interface IRequirementRepository : IRepositoryBase<Requirement> { }
public interface IProjectImagesRepostiory : IRepositoryBase<ProjectImage> { }
  