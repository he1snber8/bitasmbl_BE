using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Repositories;

public abstract class DbEntitySetter<TEntity, TDbContext>(TDbContext context)
    where TEntity : class, IBaseEntity
    where TDbContext : DbContext
{
    protected readonly TDbContext Context = context ?? throw new ArgumentNullException(nameof(context));
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();
}