using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Authentication.Authorization;


namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("commands/[controller]")]
public class UserCommandController : Controller
{

    private readonly ILogger<UserCommandController> _logger;
    private readonly IAuthorizableService _authorizableService;

    public UserCommandController(ILogger<UserCommandController> logger,
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
            await _authorizableService.Register(model,HttpContext);

            return Ok("Yay, User registered successfully!");
        }
        catch (IdentityException ex)
        {
            _logger.LogInformation("{Date}: identity format error: {errorMessage}",
               DateTime.Now, ex);

            return BadRequest($"format error, please check your credentials again!");
        }
        catch (EmailValidationException ex)
        {
            _logger.LogInformation("{Date}: entity with given email already exists error: {errorMessage}",
               DateTime.Now, ex);

            return BadRequest($"user with given email already exists");
        }

    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] UserLoginModel loginModel)
    {
        try
        {
            await _authorizableService.AuthenticateLogin(loginModel,HttpContext);

            return Ok("Authenticated Successfully!");
        }
        catch (UserLoginException ex)
        {
            _logger.LogInformation("{Date}: Validating login has failed: {errorMessage}",
               DateTime.Now, ex);
            return BadRequest("Login error, check the credentials");
        }

    }

    [HttpPost("refresh")]
    //[Authorize(Policy = "AdminOrUser")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest refreshRequest)
    {
        try
        {
            await _authorizableService.RefreshToken(refreshRequest.accessToken, refreshRequest.refreshToken,HttpContext);

            return Ok("Token refreshed successfully!");
        }
        catch (TokenValidationException ex)
        {
            _logger.LogWarning(ex, "Token expired.");
            return Unauthorized();
        }
    }

    //[Authorize(Policy = "AdminOrUser")]
    //public async Task<IActionResult> LogOut()
    //{
    //    try
    //    {
    //        var userId = User.FindFirstValue("Id");

    //        await _refreshTokenCommand.InvalidateUserToken(userId!);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogInformation("{Date}: error while logging out: {errorMessage}",
    //           DateTime.Now, ex.Message);
    //    }

    //    return Ok("Logged out!");
    //}

    //[HttpDelete("delete/{id:int}")]
    //[Authorize(Policy = "AdminOnly")]
    //public virtual async Task<IActionResult> Delete([FromRoute] int id)
    //{
    //    try
    //    {
    //        await _userCommandService.Delete(id);
    //    }
    //    catch (DbUpdateException ex)
    //    {
    //        return BadRequest($"Database failed: {ex.Message}");
    //    }
    //    catch (InvalidOperationException ex)
    //    {
    //        return BadRequest($"Invalid operation: {ex.Message}");
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest($"Delete operation failed: {ex.Message}");
    //    }
    //    return Ok("Entity deleted successfully!");
    //}


}


//[ApiController]
//[Route("commands/[controller]")]
//public class AdminController : Controller
//{
//    private readonly IUserCommandService _userCommandService;
//    private readonly AdminTokenGenerator _tokenGenerator;
//    private readonly AdminRefreshTokenGenerator _refreshTokenGenerator;

//    public AdminController(IUserCommandService userCommandService, AdminTokenGenerator adminTokenGenerator, AdminRefreshTokenGenerator adminRefreshTokenGenerator)
//    {
//        _userCommandService = userCommandService;
//        _tokenGenerator = adminTokenGenerator;
//        _refreshTokenGenerator = adminRefreshTokenGenerator;
//    }

//    [HttpPost("RegisterAdmin")]
//    public async Task<IActionResult> SetUpAdmin([FromBody] RegisterUserModel model)
//    {
//        try
//        {
//           return Ok(await _userCommandService.Register(model));
//        }
//        catch (Exception)
//        {
//            return BadRequest();
//        }
//    }

//    [HttpPost("RegisterAdmin")]
//    public async Task<IActionResult> LoginAdmin([FromBody] UserLoginModel model)
//    {
//        try
//        {
//            (bool isAuthenticated, UserModel admin) = await _userCommandService.AutheticateLogin(model);

//            if (isAuthenticated)
//            {
//                (string freshToken, string refreshToken) =
//                    (_tokenGenerator.GenerateToken(admin),
//                    _refreshTokenGenerator.GenerateRefreshToken());

//                _tokenGenerator.GenerateToken(admin);
//                return Ok(new AuthenticatedAdminResponse(freshToken, refreshToken));
//            }

//            return Unauthorized();
//        }
//        catch (Exception)
//        {
//            return BadRequest();
//        }
//    }
//}
