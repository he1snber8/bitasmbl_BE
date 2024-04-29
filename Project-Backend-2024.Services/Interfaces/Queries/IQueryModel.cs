using Project_Backend_2024.DTO;
using System.Linq.Expressions;

namespace Project_Backend_2024.Services.Interfaces.Queries;

public interface IQueryModel<TEntity>
    where TEntity : class, IEntity
{
    TEntity GetById(int id);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> SetMany(Expression<Func<TEntity, bool>> predicate);
}
