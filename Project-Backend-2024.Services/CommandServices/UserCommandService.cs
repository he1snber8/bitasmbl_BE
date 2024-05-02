using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.BasicOperations;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Services.CommandServices;

public class UserCommandService : BaseCommandService<UserModel, User, IUserRepository>, IUserCommandService
{
    public UserCommandService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository repository) : base(unitOfWork, mapper, repository) { }

    public override async Task<int> Insert(UserModel model)
    {
        if (!model.ValidateUsername() || _repository.Set(m => m.Username == model.Username).SingleOrDefault() != null) throw new UsernameValidationException("Username is either in wrong format or is already in use, please ");
        if (!model.ValidatePassword()) throw new PasswordValidationException("Password is in wrong format, please check again!");
        if (!model.ValidateEmail() || _repository.Set(m => m.Email == model.Email).SingleOrDefault() != null) throw new EmailValidationException("Email is either in wrong format or is already registered, please check again!");

        return await base.Insert(model);
    }

    public (bool,User) AutheticateLogin(IAuthenticatable loginModel)
    {
        User user = _repository.Set(m => m.Username == loginModel.Username).SingleOrDefault() ?? throw new ArgumentNullException();

        return (loginModel.Password.HashEquals(user.Password), user);
    }
}
