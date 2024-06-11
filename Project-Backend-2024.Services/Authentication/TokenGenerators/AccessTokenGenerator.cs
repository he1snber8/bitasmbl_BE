using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.TokenGenerators;

public class AccessTokenGenerator : TokenGenerator
{
    private readonly AuthConfiguration _authConfiguration;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessTokenGenerator(AuthConfiguration authConfiguration,UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _authConfiguration = authConfiguration;
        _userManager = userManager;
         _httpContextAccessor = httpContextAccessor;
    }

    public async Task GenerateAndSignIn(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
        new Claim("Id", user.Id),
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim (ClaimTypes.Name, user.UserName!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claimsIdentity = new ClaimsIdentity(claims, "MyAuthScheme");

        await _httpContextAccessor.HttpContext
            .SignInAsync("MyAuthScheme",
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties());
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync("MyAuthScheme");
    }
}
