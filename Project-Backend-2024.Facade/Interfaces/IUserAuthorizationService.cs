using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AlterModels;

namespace Project_Backend_2024.Facade.Interfaces;

public interface IUserAuthentication
{
    Task<User> AuthenticateLogin(UserLoginModel model);
}

public interface IUserRegistration
{
    Task Register(RegisterUserModel registerModel);
}

public interface IUserSignOut
{
    Task SignOutAsync();
}