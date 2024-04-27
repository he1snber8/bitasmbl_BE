

using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.Interfaces.Commands;

public interface ICommandModel<TCommandModel>
    where TCommandModel : class, IBasicModel
{
    Task<int> Insert(TCommandModel model);
    Task Update(int id, TCommandModel model);
    Task<int> Delete(int id);
}
