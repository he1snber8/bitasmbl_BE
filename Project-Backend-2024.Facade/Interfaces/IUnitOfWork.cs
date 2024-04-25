namespace Project_Backend_2024.Facade.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IProjectRepository ProjectRepository { get; }
    IUserSkillsRepository UserSkillsRepository { get; }
    ISkillRepository SkillRepository { get; }
    IAppliedProjectRepository AppliedProjectRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}
