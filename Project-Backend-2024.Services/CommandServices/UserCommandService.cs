using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.BasicOperations;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class UserCommandService : BaseCommandService<UserModel, User, IUserRepository>, IUserCommandService
{
    public UserCommandService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository repository) : base(unitOfWork, mapper, repository) { }

    public override async Task<int> Insert(UserModel model)
    {
        if (!model.ValidateUsername()) throw new Exception(model.Username);
        if (!model.ValidatePassword()) throw new Exception(model.Password);
        if (!model.ValidateEmail()) throw new Exception(model.Email);

        return await base.Insert(model);
    }
}
