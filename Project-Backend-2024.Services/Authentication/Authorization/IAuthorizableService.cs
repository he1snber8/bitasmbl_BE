using Microsoft.AspNetCore.Http;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Authentication.Authorization;

public interface IAuthorizableService
{
    Task Register(RegisterUserModel registerModel, HttpContext httpContext);
    Task AuthenticateLogin(UserLoginModel model, HttpContext httpContext);
    Task RefreshToken(string expiredAccessToken, string refreshToken, HttpContext httpContext);
}