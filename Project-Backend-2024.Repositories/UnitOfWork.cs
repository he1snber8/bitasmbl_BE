using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Repositories;

public class UnitOfWork(DatabaseContext context, ILogger<UnitOfWork> logger) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;
    private readonly DatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<UnitOfWork> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly Lazy<IUserRepository> _userRepository = new(() => new UserRepository(context));
    private readonly Lazy<IProjectRepository> _projectRepository = new(() => new ProjectRepository(context));

    private readonly Lazy<IUserSkillsRepository> _userSkillsRepository = new(() => new UserSkillsRepository(context));
    private readonly Lazy<ISkillRepository> _skillRepository = new(() => new SkillRepository(context));
    
    private readonly Lazy<ITransactionRepository> _transactionRepository = new(() => new TransactionRepository(context));

    private readonly Lazy<ICategoryRepository> _categoryRepository =
        new(() => new CategoryRepository(context));

    private readonly Lazy<IRequirementRepository> _requirementRepository =
        new(() => new RequirementRepository(context));
    
    private readonly Lazy<IProjectImagesRepostiory> _projectImagesRepository =
        new(() => new ProjectImagesRepository(context));
    
    public IUserRepository UserRepository => _userRepository.Value;
    public IProjectRepository ProjectRepository => _projectRepository.Value;
    public IRequirementRepository RequirementRepository => _requirementRepository.Value;
    public ICategoryRepository CategoryRepository => _categoryRepository.Value;
    public IUserSkillsRepository UserSkillsRepository => _userSkillsRepository.Value;
    public ISkillRepository SkillRepository => _skillRepository.Value;
    public IProjectImagesRepostiory ProjectImagesRepository => _projectImagesRepository.Value;
    public ITransactionRepository TransactionRepository => _transactionRepository.Value;

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