using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Users;

public class UserSignOutCommand(
    IHttpContextAccessor httpContextAccessor) : IUserSignOut
{
    public async Task SignOutAsync()
    {
        await httpContextAccessor.HttpContext.SignOutAsync("Cookies");
    }
}