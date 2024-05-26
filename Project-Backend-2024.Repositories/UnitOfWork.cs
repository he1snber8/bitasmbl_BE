using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Project_Backend_2024.Facade.Interfaces;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;
    private readonly DatabaseContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    //private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IProjectRepository> _projectRepository;
    private readonly Lazy<IUserSkillsRepository> _userSkillsRepository;
    private readonly Lazy<ISkillRepository> _skillRepository;
    private readonly Lazy<IAppliedProjectRepository> _appliedProjectRepository;

    public UnitOfWork(DatabaseContext context, ILogger<UnitOfWork> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //_userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
        _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(context));
        _userSkillsRepository = new Lazy<IUserSkillsRepository>(() => new UserSkillsRepository(context));
        _skillRepository = new Lazy<ISkillRepository>(() => new SkillRepository(context));
        _appliedProjectRepository = new Lazy<IAppliedProjectRepository>(() => new AppliedProjectRepository(context));
    }

    //public IUserRepository UserRepository => _userRepository.Value;
    public IProjectRepository ProjectRepository => _projectRepository.Value;
    public IUserSkillsRepository UserSkillsRepository => _userSkillsRepository.Value;
    public ISkillRepository SkillRepository => _skillRepository.Value;
    public IAppliedProjectRepository AppliedProjectRepository => _appliedProjectRepository.Value;

    public void BeginTransaction()
    {
        try
        {
            _transaction = _context.Database.BeginTransaction();
            _logger.LogInformation("Transaction started at:{@DateTimeUtc}", DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to begin transaction");
            throw;
        }
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
            _transaction?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to commit transaction");
            throw;
        }
    }

    public void Dispose()
    {
        try
        {
            Rollback();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to rollback transaction, transaction started at:{@DateTimeUtc}", DateTime.Now);
            throw;
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }

    public void SaveChanges()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DbContext error");
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DbContext error while saving changes");
            throw;
        }
    }
}
