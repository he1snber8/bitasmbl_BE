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
        if (loginModel.LoginType != null &&
            RegistrationType.Google.ToString().ToLower() != loginModel.LoginType.ToLower() &&
            RegistrationType.Github.ToString().ToLower() != loginModel.LoginType.ToLower() &&
            RegistrationType.Standard.ToString().ToLower() != loginModel.LoginType.ToLower())
        {
            throw new Exception("SSO format is incorrect");
        }
        
        if (RegistrationType.Github.ToString().ToLower() != loginModel.LoginType.ToLower() &&
            !EmailFormatValidator.IsValidEmail(loginModel!.Email))
            throw new EmailValidationException();

        if (RegistrationType.Github.ToString().ToLower() == loginModel.LoginType.ToLower())
        {
            var githubUser = await userManager.FindByNameAsync(loginModel!.Username)
                       ?? throw new UserLoginException(loginModel.Username);
            
            
            
            if (githubUser.LockoutEnd.HasValue && githubUser.LockoutEnd.Value > DateTimeOffset.UtcNow)
                throw new UserLockedException(githubUser.Email);

            githubUser.LastLogin = DateTime.Now;
            
            // await userManager.AddToRoleAsync(githubUser, "User");
            await userManager.UpdateAsync(githubUser);

            await cookieGenerator.GenerateCookieAndSignIn(githubUser);

            return githubUser;
        }

        var user = await userManager.FindByEmailAsync(loginModel!.Email)
                   ?? throw new UserLoginException(loginModel.Email);

        // Skip password check for Google or Github users
        // Check password only if it is standard type
        if (RegistrationType.Standard.ToString().ToLower() == loginModel.LoginType.ToLower() &&
            (string.IsNullOrWhiteSpace(loginModel.Password) ||
             !await userManager.CheckPasswordAsync(user, loginModel.Password)))
        {
            await userManager.AccessFailedAsync(user);
            throw new UserLoginException(user.UserName, true);
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            throw new UserLockedException(user.Email);

        user.LastLogin = DateTime.Now;
        
        // await userManager.AddToRoleAsync(user, "User");
        await userManager.UpdateAsync(user);

        await cookieGenerator.GenerateCookieAndSignIn(user);

        return user;
    }
}