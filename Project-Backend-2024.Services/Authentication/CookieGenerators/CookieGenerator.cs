using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Authentication.TokenGenerators;

public class CookieGenerator(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task GenerateCookieAndSignIn(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
        new ("Id", user.Id),
        new (ClaimTypes.Email, user.Email!),
        new (ClaimTypes.Name, user.UserName!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Keeps the cookie valid even after the browser is closed
            ExpiresUtc = DateTimeOffset.Now.AddMinutes(5)
        };

        await httpContextAccessor.HttpContext
            .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

}
