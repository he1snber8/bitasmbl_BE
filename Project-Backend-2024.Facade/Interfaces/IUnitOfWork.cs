namespace Project_Backend_2024.Facade.Interfaces;

public interface IUnitOfWork
{
    IProjectRepository ProjectRepository { get; }
    IUserSkillsRepository UserSkillsRepository { get; }
    ISkillRepository SkillRepository { get; }
    IProjectApplicationRepository ProjectApplicationRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}
