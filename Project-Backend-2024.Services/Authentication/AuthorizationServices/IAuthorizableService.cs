using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using System.Net.Http;

namespace Project_Backend_2024.Services.Authentication.Authorization;

public interface IAuthorizableService
{
    Task Register(RegisterUserModel registerModel);
    Task AuthenticateLogin(UserLoginModel model);
    Task RefreshToken(string accessToken);
}