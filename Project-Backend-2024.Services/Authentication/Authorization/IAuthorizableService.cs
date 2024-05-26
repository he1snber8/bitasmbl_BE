
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Authentication.Authorization;

public interface IAuthorizableService
{
    Task<(bool, UserModel)> AutheticateLogin(IAuthenticatable loginModel);
    Task Register(RegisterUserModel registerModel);
    Task<bool> ValidateCredentials(UserModel model);
}