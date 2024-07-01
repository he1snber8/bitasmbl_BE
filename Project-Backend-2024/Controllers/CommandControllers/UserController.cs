using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Authentication.AuthorizationServices;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("commands/[controller]")]
public class UsersController(
    ILogger<UsersController> logger,IUserAuthorizationService userAuthorizationService, 
    IMemoryCache memoryCache,UserManager<User> userManager) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromQuery] string username ,[FromQuery] string email,[FromQuery] string password)
    {
        try
        {
            var registerModel = new RegisterUserModel { Username = username, Email = email, Password = password };
            
            await userAuthorizationService.Register(registerModel);
            
            logger.LogInformation("{Date}: User registered successfully.", DateTime.Now);
            return Ok("User registered successfully, please check your email for confirmation");
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
        
        catch (EntityAlreadyExistsException ex)
        {
            logger.LogError("{Date}: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromQuery] string username,[FromQuery] string password)
    {
        try
        {
            var userLogin = new UserLoginModel { Username = username, Password = password };
            
            await userAuthorizationService.AuthenticateLogin(userLogin);
            
            logger.LogInformation("{Date}: User authenticated successfully.", DateTime.Now);

            return Ok("Authenticated successfully!");
        }
        catch (UserLoginException ex)
        {
            logger.LogWarning("{Date}: Login failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Login error, check the credentials.");
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

            var cookie = HttpContext.Request.Cookies[".AspNetCore.Cookies"] 
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
            memoryCache.Remove($"key-{User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value}");
            
            Console.WriteLine("cache removed!");
            return Ok("Logged out!");
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Error while logging out: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Error while logging out");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost("lock")]
    public async Task<IActionResult> LockoutUser([FromQuery]string userId, DateTime lockoutEnd)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null) return BadRequest("Failed to lock out user.");
        
        var result = await userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        
        if (result.Succeeded)
        {
            return Ok("User locked out until " + lockoutEnd);
        }
        return BadRequest("Failed to lock out user.");
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost("unlock")]
    public async Task<IActionResult> UnlockUser([FromQuery]string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        
        if (user == null) return BadRequest("Failed to unlock user.");
        
        var result = await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
        
        if (result.Succeeded)
        {
            return Ok("User unlocked.");
        }
        return BadRequest("Failed to unlock user.");
    }

}

