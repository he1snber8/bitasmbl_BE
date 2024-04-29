using Project_Backend_2024.Services.Models;
using System.Linq.Expressions;

namespace Project_Backend_2024.Services.Interfaces.Queries;

public interface IQueryModel<TQueryModel>
    where TQueryModel : class, IEntityModel
{
    TQueryModel GetById(int id);
    IQueryable<TQueryModel> Set(Expression<Func<TQueryModel, bool>> predicate);
    IEnumerable<TQueryModel> Set();
}
