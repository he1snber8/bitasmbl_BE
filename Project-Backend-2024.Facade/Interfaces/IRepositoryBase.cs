using Project_Backend_2024.DTO.Interfaces;
using System.Linq.Expressions;

namespace Project_Backend_2024.Facade.Interfaces;

public interface IRepositoryBase<TEntity>
    where TEntity : IEntity
{
    void Insert(TEntity entity);
    void Delete(TEntity entity);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Set();
    void Update(TEntity entity);
    void Delete(object id);
    Task<TEntity?> Get(params object[] id);
    Task<List<TEntity>> GetAll();
}
