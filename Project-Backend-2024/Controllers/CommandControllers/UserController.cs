using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Authentication.Authorization;
using System.Net;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("commands/[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IAuthorizableService _authorizableService;

    public UserController(ILogger<UserController> logger,
        IAuthorizableService authorizableService)
    {
        _logger = logger;
        _authorizableService = authorizableService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        try
        {
            await _authorizableService.Register(model);
            _logger.LogInformation("{Date}: User registered successfully.", DateTime.Now);
            return Ok("User registered successfully!");
        }
        catch (IdentityException ex)
        {
            _logger.LogWarning("{Date}: Identity error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Format error, please check your credentials again!");
        }
        catch (EmailValidationException ex)
        {
            _logger.LogWarning("{Date}: Email validation error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("User with given email already exists.");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Date}: Unexpected error: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] UserLoginModel loginModel)
    {
        try
        {
            await _authorizableService.AuthenticateLogin(loginModel);
            _logger.LogInformation("{Date}: User authenticated successfully.", DateTime.Now);
            return Ok("Authenticated successfully!");
        }
        catch (UserLoginException ex)
        {
            _logger.LogWarning("{Date}: Login failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Login error, check the credentials.");
        }
        catch (TokenValidationException ex)
        {
            _logger.LogWarning("{Date}: Token validation error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Token invalid, refresh it or something.");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Date}: Unexpected error: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, "Internal server error.");
        }
    }

    [Authorize(AuthenticationSchemes = "MyAuthScheme", Policy = "AdminOrUser")]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        try
        {
            if (User.Identity is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            if (User.Identity.IsAuthenticated is false)
            {
                return Unauthorized("User is not authenticated.");
            }

            var cookie = HttpContext.Request.Cookies[".AspNetCore.MyAuthScheme"] ?? throw new CookieException();

            await _authorizableService.RefreshToken(cookie);
            _logger.LogInformation("{Date}: Token refreshed successfully.", DateTime.Now);
            return Ok("Refreshed successfully!");
        }
        catch (CookieException ex)
        {
            _logger.LogWarning("{Date}: Cookie error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Access token not found in cookies.");
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("{Date}: Unauthorized access: {errorMessage}", DateTime.Now, ex.Message);
            return Unauthorized("User is not authenticated.");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Date}: Unexpected error: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("log-out")]
    [Authorize(AuthenticationSchemes = "MyAuthScheme", Policy = "AdminOrUser")]
    public async Task<IActionResult> LogOut()
    {
        try
        {
            await HttpContext.SignOutAsync("MyAuthScheme");
            _logger.LogInformation("{Date}: User logged out successfully.", DateTime.Now);
            return Ok("Logged out!");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Date}: Error while logging out: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest("Error while logging out");
        }
    }


}

