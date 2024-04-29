using Project_Backend_2024.DTO;
using System.Linq.Expressions;

namespace Project_Backend_2024.Facade.Interfaces;

public interface IRepositoryBase <TEntity>
    where TEntity : IEntity
{
    void Insert(TEntity entity);
    void Delete(TEntity entity);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    void Update(TEntity entity);
    void Delete(object id);
    Task<TEntity?> Get(params object[] id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> SetMany(Expression<Func<TEntity, bool>> predicate);
}
