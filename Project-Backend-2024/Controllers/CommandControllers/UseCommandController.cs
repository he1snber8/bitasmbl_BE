using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Facade.Responses;
using Project_Backend_2024.Services.TokenGenerators;
using Project_Backend_2024.Services.TokenValidators;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Project_Backend_2024.Services.Interfaces.Queries;
using System.Reflection.Metadata.Ecma335;
using Project_Backend_2024.Services.Authentication.Authorization;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("commands/[controller]")]
public class UserCommandController : Controller
{
    private readonly IUserCommandService _userCommandService;
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly UserRefreshTokenGenerator _refreshTokenGenerator;
    private readonly RefreshTokenValidator _refreshTokenValidator;
    private readonly IRefreshTokenCommandService _refreshTokenCommand;
    //private readonly IRefreshTokenQueryService _refreshTokenQueryService;
    //private readonly IUserQueryService _userQueryService;
    private readonly UserConfiguration _authConfiguration;
    private readonly ILogger<UserCommandController> _logger;

    public UserCommandController(IUserCommandService userCommandService, AccessTokenGenerator accessTokenGenerator,
        UserRefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator,
        IRefreshTokenCommandService refreshTokenCommand, UserConfiguration authConfiguration,
        IRefreshTokenQueryService refreshTokenQueryService, IUserQueryService userQueryService, ILogger<UserCommandController> logger)
    {
        _userCommandService = userCommandService;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenValidator = refreshTokenValidator;
        _refreshTokenCommand = refreshTokenCommand;
        _authConfiguration = authConfiguration;
        //_refreshTokenQueryService = refreshTokenQueryService;
        //_userQueryService = userQueryService;
        _logger = logger;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        try
        {
            await _userCommandService.Register(model);
            return Ok("User registered successfully!");
        }
        catch (UsernameValidationException ex)
        {
            _logger.LogInformation("{Date}: username format error: {errorMessage}",
               DateTime.Now, ex.Message);

            return BadRequest($"Username format is invalid");
        }
        catch (PasswordValidationException ex)
        {
            _logger.LogInformation("{Date}: password format error: {errorMessage}",
               DateTime.Now, ex.Message);

            return BadRequest($"Password format is invalid");
        }
        catch (EmailValidationException ex)
        {
            _logger.LogInformation("{Date}: email format error: {errorMessage}",
               DateTime.Now, ex.Message);

            return BadRequest($"Email format is invalid");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] UserLoginModel loginModel)
    {
        (bool isAuthenticated, UserModel user) = await _userCommandService.AutheticateLogin(loginModel);
        try
        {
            if (isAuthenticated)
            {
                (string freshToken, string refreshToken) =
                    (_accessTokenGenerator.GenerateToken(user),
                    _refreshTokenGenerator.GenerateRefreshToken());

                (var token, bool isAltered) = await _refreshTokenCommand.UpdateUserToken(
                    user.Id,
                    DateTime.Now.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),
                    refreshToken);

                if (isAltered) return Ok(new AuthenticatedUserResponse(freshToken, refreshToken));

                else return Ok(new AuthenticatedUserResponse(freshToken, token.Token));

            }

            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("{Date}: error while logging out: {errorMessage}",
               DateTime.Now, ex.Message);

            return BadRequest();
        }
    }

    [HttpPost("refresh")]
    [Authorize(Policy = "AdminOrUser")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest refreshRequest)
    {
        if (!_refreshTokenValidator.Validate(refreshRequest.refreshToken)) return Unauthorized();

        var user = await _refreshTokenCommand.GetUserByToken(refreshRequest);

        return user == null ? Unauthorized() : Ok(new AccessToken(_accessTokenGenerator.GenerateToken(user)));
    }

    [Authorize(Policy = "AdminOrUser")]
    public async Task<IActionResult> LogOut()
    {
        try
        {
            var userId = User.FindFirstValue("Id");

            int.TryParse(userId, out int tokenId);

            await _refreshTokenCommand.InvalidateUserToken(tokenId);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("{Date}: error while logging out: {errorMessage}",
               DateTime.Now, ex.Message);
        }

        return Ok("Logged out!");
    }

    [HttpDelete("delete/{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public virtual async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _userCommandService.Delete(id);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"Database failed: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest($"Invalid operation: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Delete operation failed: {ex.Message}");
        }
        return Ok("Entity deleted successfully!");
    }


}


[ApiController]
[Route("commands/[controller]")]
public class AdminController : Controller
{
    private readonly IUserCommandService _userCommandService;
    private readonly AdminTokenGenerator _tokenGenerator;
    private readonly AdminRefreshTokenGenerator _refreshTokenGenerator;

    public AdminController(IUserCommandService userCommandService, AdminTokenGenerator adminTokenGenerator, AdminRefreshTokenGenerator adminRefreshTokenGenerator)
    {
        _userCommandService = userCommandService;
        _tokenGenerator = adminTokenGenerator;
        _refreshTokenGenerator = adminRefreshTokenGenerator;
    }

    [HttpPost("RegisterAdmin")]
    public async Task<IActionResult> SetUpAdmin([FromBody] RegisterUserModel model)
    {
        try
        {
           return Ok(await _userCommandService.Register(model));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("RegisterAdmin")]
    public async Task<IActionResult> LoginAdmin([FromBody] UserLoginModel model)
    {
        try
        {
            (bool isAuthenticated, UserModel admin) = await _userCommandService.AutheticateLogin(model);

            if (isAuthenticated)
            {
                (string freshToken, string refreshToken) =
                    (_tokenGenerator.GenerateToken(admin),
                    _refreshTokenGenerator.GenerateRefreshToken());

                _tokenGenerator.GenerateToken(admin);
                return Ok(new AuthenticatedAdminResponse(freshToken, refreshToken));
            }

            return Unauthorized();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
