using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Repositories;

// public class UserRepository(DatabaseContext context) : RepositoryBase<User>(context), IUserRepository;

public class TeamManagerRepository(DatabaseContext context) : RepositoryBase<TeamManager>(context), ITeamManagerRepository;
public class OrganizationManagerRepository(DatabaseContext context) : RepositoryBase<OrganizationManager>(context), IOrganizationManagerRepository;

// public class UserSkillsRepository(DatabaseContext context) : RepositoryBase<UserSkills>(context), IUserSkillsRepository;
// public class SkillRepository(DatabaseContext context) : RepositoryBase<Skill>(context), ISkillRepository;

public class TransactionRepository(DatabaseContext context) : RepositoryBase<Transaction>(context), ITransactionRepository;
public class ProjectRepository(DatabaseContext context) : RepositoryBase<Project>(context), IProjectRepository;
public class CategoryRepository(DatabaseContext context) : RepositoryBase<Category>(context), ICategoryRepository;

public class OrganizationRepository(DatabaseContext context) : RepositoryBase<Organization>(context), IOrganizationRepository;
public class TeamRepository(DatabaseContext context) : RepositoryBase<Team>(context), ITeamRepository;

public class ProjectImagesRepository(DatabaseContext context) : RepositoryBase<ProjectImage>(context), IProjectImagesRepostiory;

public class RequirementRepository(DatabaseContext context) : RepositoryBase<Requirement>(context), IRequirementRepository;