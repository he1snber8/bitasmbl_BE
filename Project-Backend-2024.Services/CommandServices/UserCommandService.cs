
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class UserCommandService : BaseCommandService<UserModel, User, IUserRepository>, IUserCommand
{
    public UserCommandService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository repository) : base(unitOfWork, mapper, repository)
    {
    }

    public override Task<int> Insert(UserModel model)
    {
        if (model.Username.Length < 5) throw new Exception(model.Username);
        if (model.Password.Length < 5 || !model.Password.Any(char.IsDigit)) throw new Exception(model.Password);
        if (model.Email.Length < 8 || !model.Email.Contains('@')) throw new Exception(model.Email);

        return base.Insert(model);
    }

    public override async Task<int> Delete(int id) => await base.Delete(id);

    public override async Task Update(int id, UserModel model)
    {
        await base.Update(id, model);
    }
}