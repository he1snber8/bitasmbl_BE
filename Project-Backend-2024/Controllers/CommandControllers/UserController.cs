using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Facade.Responses;
using Project_Backend_2024.Services.TokenGenerators;

namespace Project_Backend_2024.Controllers.CommandControllers;

public class UserController : BaseCommandController<UserModel, IUserCommandService>
{
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;

    public UserController(IUserCommandService command, AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator)
        : base(command)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }


    [HttpPost("register")]
    public override async Task<IActionResult> Insert([FromBody] UserModel model)
    {
        try
        {
            return await base.Insert(model);
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
    public IActionResult LogIn([FromBody] UserLoginModel loginModel)
    {
        (bool isAuthenticated, User user) = _commandService.AutheticateLogin(loginModel);

        if (isAuthenticated)
        {
            string token = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            return Ok(new AuthenticatedUserResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }

        return Unauthorized();
    }

}


