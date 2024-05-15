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

    public UserController(IUserCommandService userCommandService, AccessTokenGenerator accessTokenGenerator,
        RefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator,
        IRefreshTokenCommandService refreshTokenCommand, AuthConfiguration authConfiguration,
        IRefreshTokenQueryService refreshTokenQueryService, IUserQueryService userQueryService)
    {
        _userCommandService = userCommandService;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenValidator = refreshTokenValidator;
        _refreshTokenCommand = refreshTokenCommand;
        _authConfiguration = authConfiguration;
        _refreshTokenQueryService = refreshTokenQueryService;
        _userQueryService = userQueryService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Insert([FromBody] RegisterUserModel model)
    {
        try
        {
            await _userCommandService.Register(model);
            return Ok("User registered successfully!");
        }
        catch (UsernameValidationException ex)
        {
            return BadRequest($"Username validation failed: {ex.Message}");
        }
        catch (PasswordValidationException ex)
        {
            return BadRequest($"Password validation failed: {ex.Message}");
        }
        catch (EmailValidationException ex)
        {
            return BadRequest($"Email validation failed: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] UserLoginModel loginModel)
    {
        (bool isAuthenticated, User user) = await _userCommandService.AutheticateLogin(loginModel);

        if (isAuthenticated)
        {
            (string freshToken, string refreshToken) =
            (_accessTokenGenerator.GenerateToken(user), 
            _refreshTokenGenerator.GenerateRefreshToken());

            (var token,bool isAltered) = await _refreshTokenCommand.UpdateUserToken(user.Id,
            DateTime.Now.AddMinutes(_authConfiguration.RefreshTokenExpirationMinutes),refreshToken);

            if (isAltered) return Ok(new AuthenticatedUserResponse(freshToken, refreshToken));

            else return Ok(new AuthenticatedUserResponse(freshToken, token.Token));
        }

        return Unauthorized();
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
        var userId = User.FindFirstValue("Id");

        int.TryParse(userId, out int tokenId);

        await _refreshTokenCommand.InvalidateUserToken(tokenId);

        return Ok("Logged out!");
    }
}



