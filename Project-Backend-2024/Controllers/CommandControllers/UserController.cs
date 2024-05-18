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

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("commands/[controller]")]
public class UserController : Controller
{
    private readonly IUserCommandService _userCommandService;
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;
    private readonly RefreshTokenValidator _refreshTokenValidator;
    private readonly IRefreshTokenCommandService _refreshTokenCommand;
    private readonly IRefreshTokenQueryService _refreshTokenQueryService;
    private readonly IUserQueryService _userQueryService;
    private readonly AuthConfiguration _authConfiguration;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserCommandService userCommandService, AccessTokenGenerator accessTokenGenerator,
        RefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator,
        IRefreshTokenCommandService refreshTokenCommand, AuthConfiguration authConfiguration,
        IRefreshTokenQueryService refreshTokenQueryService, IUserQueryService userQueryService, ILogger<UserController> logger)
    {
        _userCommandService = userCommandService;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenValidator = refreshTokenValidator;
        _refreshTokenCommand = refreshTokenCommand;
        _authConfiguration = authConfiguration;
        _refreshTokenQueryService = refreshTokenQueryService;
        _userQueryService = userQueryService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        try
        {
            await _userCommandService.Register(model);

            _logger.LogInformation("{Date}: New user registered: {Username}",
               DateTime.Now, model.Username);

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
    //[HasPermission(Permission.WriteAccess,Permission.ReadAccess)]
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
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest refreshRequest)
    {
        if (!_refreshTokenValidator.Validate(refreshRequest.refreshToken)) return Unauthorized();

        var user = await _refreshTokenCommand.GetUserByToken(refreshRequest);

        return user == null ? Unauthorized() : Ok(new AccessToken(_accessTokenGenerator.GenerateToken(user)));
    }

    [Authorize]
    [HttpDelete("logout")]
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
}



