using Project_Backend_2024.DTO;
using System.Linq.Expressions;

namespace Project_Backend_2024.Facade.Interfaces;

public interface IRepositoryBase <TEntity>
    where TEntity : IBasic
{
    void Insert(TEntity entity);
    void Delete(TEntity entity);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    void Update(TEntity entity);
    IQueryable<TEntity> Set();
    void Delete(object id);
    TEntity? Get(params object[] id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> SetAsNoTracking(Expression<Func<TEntity, bool>> predicate);
}
