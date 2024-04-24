using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;

public abstract class DbEntitySetter<TEntity, TDbContext>
    where TEntity : class, IBasic
    where TDbContext : DbContext
{
    protected readonly TDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public DbEntitySetter(TDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }
}