using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Interfaces;
using System.Linq.Expressions;

namespace Project_Backend_2024.Repositories
{
    public abstract class RepositoryBase<TEntity> : DbEntitySetter<TEntity, DatabaseContext>, IRepositoryBase<TEntity>
    where TEntity : class, IBaseEntity
    {
        public RepositoryBase(DatabaseContext context) : base(context) { }

        public async Task<TEntity?> Get(params object[] id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return await DbSet.FindAsync(id);
        }

        public IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate) => DbSet.Where(predicate);

        public IQueryable<TEntity> Set() => DbSet;

        public void Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            DbSet.Update(entity);
        }

        public void Delete(object id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            Delete(Get(id));
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            DbSet.Remove(entity);
        }

        public async Task<List<TEntity>> GetAll() => await Context.Set<TEntity>().AsNoTracking().ToListAsync();
    
    }
}
