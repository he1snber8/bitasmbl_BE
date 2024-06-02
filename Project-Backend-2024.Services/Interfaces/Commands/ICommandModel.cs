using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Interfaces.Commands;

public interface ICommandModel<TCommandModel>
    where TCommandModel : class, IEntityModel
{
    Task<int> Insert(TCommandModel model);
    Task Update(int id, TCommandModel model);
    Task<int> Delete(int id);
}
