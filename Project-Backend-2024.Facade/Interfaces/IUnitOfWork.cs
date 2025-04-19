namespace Project_Backend_2024.Facade.Interfaces;

public interface IUnitOfWork
{
    IProjectRepository ProjectRepository { get; }
    // IUserSkillsRepository UserSkillsRepository { get; }
    // ISkillRepository SkillRepository { get; }
    // IProjectApplicationRepository ProjectApplicationRepository { get; }
    // IUserRepository UserRepository { get; }
    ITeamManagerRepository TeamManagerRepository { get; }
    IOrganizationManagerRepository OrganizationManagerRepository { get; }
    
    ICategoryRepository CategoryRepository { get; }
    IRequirementRepository RequirementRepository { get; }
    // IProjectCategoryRepository ProjectCategoryRepository { get; }
    
    // IProjectRequirementRepository ProjectRequirementRepository { get; }
    
    IProjectImagesRepostiory ProjectImagesRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}
