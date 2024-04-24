using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using System.Linq.Expressions;

namespace Project_Backend_2024.Repositories
{
    public abstract class RepositoryBase<TEntity> : DbEntitySetter<TEntity, DatabaseContext>, IRepositoryBase<TEntity>
    where TEntity : class, IBasic
    {
        public RepositoryBase(DatabaseContext context) : base(context) { }

        public TEntity? Get(params object[] id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

        public IQueryable<TEntity> Set() => _dbSet;

        public void Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
        }

        public void Delete(object id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            Delete(Get(id)!);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

    }
}
