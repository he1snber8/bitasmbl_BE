using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Authentication.AuthorizationServices;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("commands/[controller]")]
public class UserController(
    ILogger<UserController> logger,
    IUserAuthorizationService userAuthorizationService, IMemoryCache memoryCache)
    : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        try
        {
            await userAuthorizationService.Register(model);
            logger.LogInformation("{Date}: User registered successfully.", DateTime.Now);
            return Ok("User registered successfully!");
        }
        catch (IdentityException ex)
        {
            logger.LogWarning("{Date}: Identity error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Format error, please check your credentials again!");
        }
        catch (EmailValidationException ex)
        {
            logger.LogWarning("{Date}: Email validation error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(ex.ToString());
        }
        catch (PasswordValidationException ex)
        {
            logger.LogError("{Date}: Password format error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(ex.ToString());
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] UserLoginModel loginModel)
    {
        try
        {
            await userAuthorizationService.AuthenticateLogin(loginModel);
            logger.LogInformation("{Date}: User authenticated successfully.", DateTime.Now);
            memoryCache.Remove("SupaCache");
            Console.WriteLine("cache removed!");
            return Ok("Authenticated successfully!");
        }
        catch (UserLoginException ex)
        {
            logger.LogWarning("{Date}: Login failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Login error, check the credentials.");
        }
        catch (TokenValidationException ex)
        {
            logger.LogWarning("{Date}: Token validation error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Token invalid, refresh it or something.");
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, "Internal server error.");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        try
        {
            if (User.Identity is null || User.Identity.IsAuthenticated is false )
                throw new UnauthorizedAccessException();

            var cookie = HttpContext.Request.Cookies[".AspNetCore.MyAuthScheme"] 
                         ?? throw new CookieException();

            await userAuthorizationService.RefreshToken(cookie);
            logger.LogInformation("[{Date}]: Token refreshed successfully.", DateTime.Now);
            return Ok("Refreshed successfully!");
        }
        catch (CookieException ex)
        {
            logger.LogWarning("{Date}: Cookie error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Access token not found in cookies.");
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning("{Date}: Unauthorized access: {errorMessage}", DateTime.Now, ex.Message);
            return Unauthorized("User is not authenticated.");
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("logout")]
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    public async Task<IActionResult> LogOut()
    {
        try
        {
            await userAuthorizationService.SignOutAsync();
            logger.LogInformation("{Date}: User logged out successfully.", DateTime.Now);
            memoryCache.Remove("SupaCache");
            Console.WriteLine("cache removed!");
            return Ok("Logged out!");
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Error while logging out: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Error while logging out");
        }
    }
}

