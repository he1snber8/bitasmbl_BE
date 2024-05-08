using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;
using System.Linq.Expressions;

namespace Project_Backend_2024.Services.Interfaces.Queries;

public interface IQueryModel<TEntity,TEntityModel>
    where TEntity : class, IEntity
    where TEntityModel : class, IEntityModel
{
    TEntityModel GetById(int id);
    IQueryable<TEntityModel> Set(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntityModel> Set();
    Task<List<TEntityModel>> GetAll();
}
