using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Authentication.CookieGenerators;
using Project_Backend_2024.Services.Formatting;

namespace Project_Backend_2024.Services.CommandServices.Users;

public class UserAuthorizationService(
    UserManager<User> userManager,
    CookieGenerator cookieGenerator) : IUserAuthentication
{
    public async Task<User> AuthenticateLogin(UserLoginModel loginModel)
    {
        var loginType = loginModel.LoginType?.ToLower()
            ?? throw new Exception("SSO format cannot be empty");

        if (
            loginType.ToLower() != RegistrationType.StandardTeamManager.ToString().ToLower() &&
            loginType.ToLower() != RegistrationType.StandardOrganizationManager.ToString().ToLower() &&
            loginType.ToLower() != RegistrationType.Google.ToString().ToLower()
        )
        {
            throw new Exception("SSO format is incorrect");
        }

        // Validate email format if not Google (can still validate for both if needed)
        if (!EmailFormatValidator.IsValidEmail(loginModel.Email))
            throw new EmailValidationException();

        // Get user by email
        var user = await userManager.FindByEmailAsync(loginModel.Email)
                   ?? throw new UserLoginException(loginModel.Email);

        // For Standard login, check password
        if (loginType == RegistrationType.StandardTeamManager.ToString().ToLower() &&
            (string.IsNullOrWhiteSpace(loginModel.Password) ||
             !await userManager.CheckPasswordAsync(user, loginModel.Password)))
        {
            await userManager.AccessFailedAsync(user);
            throw new UserLoginException(user.UserName, true);
        }

        // Check if user is locked out
        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            throw new UserLockedException(user.Email);

        await userManager.UpdateAsync(user);
        await cookieGenerator.GenerateCookieAndSignIn(user);

        return user;
    }
}