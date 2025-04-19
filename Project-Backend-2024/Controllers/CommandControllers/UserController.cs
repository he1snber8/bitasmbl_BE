using System.Net.Http.Headers;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.CommandServices.AWS;


namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("[controller]")]
public class UsersController(
    UserManager<User> userManager,
    ILogger<UsersController> logger,
    IUserAuthentication userAuthentication,
    IUserRegistration userRegistration,
    IUserSignOut userSignOut,
    IMemoryCache memoryCache,
    S3BucketService s3BucketService,
    ISender sender,
    IMapper mapper) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel registerUserModel)
    {
        try
        {
            var user = await userRegistration.Register(registerUserModel);

            var userModel = mapper.Map<UserModel>(user);

            logger.LogInformation("{Date}: User registered successfully.", DateTime.Now);
            return Ok(new
            {
                message = userModel.RegistrationType != RegistrationType.StandardTeamManager.ToString()
                    ? $"User registered successfully with {userModel.RegistrationType}"
                    : "User registered successfully, please check your email for confirmation",
                userModel
            });
        }
        catch (EmailAlreadyExistsException ex)
        {
            logger.LogError("{Date}: Email already registered redirecting to log in: {errorMessage}", DateTime.Now,
                ex.Message);
            return Ok(new { message = ex.Message });
        }
        catch (IdentityException ex)
        {
            logger.LogWarning("{Date}: Identity error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { error = "Format error, please check your credentials again!" });
        }
        catch (EmailValidationException ex)
        {
            logger.LogWarning("{Date}: Email validation error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { message = "Invalid Email" });
        }
        catch (PasswordValidationException ex)
        {
            logger.LogError("{Date}: Password format error: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { message = ex.Message });
        }

        catch (EntityAlreadyExistsException ex)
        {
            logger.LogError("{Date}: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] UserLoginModel userLogin)
    {
        try
        {
            await userAuthentication.AuthenticateLogin(userLogin);

            logger.LogInformation("{Date}: User authenticated successfully.", DateTime.Now);

            // var userModel = mapper.Map<UserModel>(user);

            return Ok(new { message = "Authenticated successfully!" });
        }
        catch (UserLoginException ex)
        {
            logger.LogWarning("{Date}: Login failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (UserLockedException ex)
        {
            logger.LogWarning("{Date}: Login failed: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(423, ex.Message);
        }
        catch (EmailValidationException ex)
        {
            logger.LogWarning("{Date}: Login failed: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(423, new { message = "Invalid email entered" });
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("logout")]
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    public async Task<IActionResult> LogOut()
    {
        try
        {
            await userSignOut.SignOutAsync();

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

    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateModel userUpdateModel)
    {
        try
        {
            await sender.Send(userUpdateModel);
            return Ok(new { message = "User updated successfully!" });
        }
        catch (UserNotFoundException ex)
        {
            logger.LogWarning("{Date}: User update failed: {errorMessage}", DateTime.Now, ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning("{Date}: User update failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { message = "Invalid operation: " + ex.Message });
        }
        catch (ArgumentNullException ex)
        {
            logger.LogWarning("{Date}: User update failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { message = "Required data is missing: " + ex.Message });
        }
        catch (PasswordValidationException ex)
        {
            logger.LogWarning("{Date}: User update failed: {errorMessage}", DateTime.Now, ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error during user update: {errorMessage}", DateTime.Now, ex.Message);
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("team/init")]
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    public async Task<IActionResult> InitializeTeam([FromForm] InitializeTeamCommand initializeTeam)
    {
        try
        {
           await sender.Send(initializeTeam);
           return Ok(new { message = "User updated successfully!" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(new { message = "Something went wrong" });
        }
    }
    
    [HttpPost("team/members/add")]
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    public async Task<IActionResult> InsertTeamMembers([FromBody] PopulateTeamWithMembersCommand populateTeamWithMembers)
    {
        try
        {
            await sender.Send(populateTeamWithMembers);
            return Ok(new { message = "Team members added successfully!" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(new { message = "Something went wrong" });
        }
    }


    // [HttpGet("auth/github")]
    // [AllowAnonymous]
    // public async Task<IActionResult> LoginGithubUser([FromQuery] string code)
    // {
    //     using var httpClient = new HttpClient();
    //
    //     var accessToken = await sender.Send(new GetGithubAccessToken(code));
    //
    //     if (string.IsNullOrEmpty(accessToken))
    //         return BadRequest("Access token missing in response.");
    //
    //     var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user");
    //     request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    //     request.Headers.UserAgent.ParseAdd("Bitasmbl"); // GitHub API requires a User-Agent
    //
    //     var userResponse = await httpClient.SendAsync(request);
    //
    //     var userResponseBody = await userResponse.Content.ReadAsStringAsync();
    //
    //     var githubUser = JsonConvert.DeserializeObject<GithubUser>(userResponseBody)
    //                      ?? throw new SerializationException();
    //
    //     var existingUser = await userManager.FindByNameAsync(githubUser.Login);
    //
    //     _ = existingUser is null
    //         ? await Register(new RegisterUserModel(Username: githubUser.Login, Email: githubUser.Email,
    //             ImageUrl: githubUser.Avatar_Url, Password: "",
    //             RegistrationType: githubUser.Provider))
    //         : await LogIn(new UserLoginModel(Email: "", githubUser.Login, Password: "",
    //             LoginType: githubUser.Provider));
    //
    //     // var repos = await sender.Send(new GetGithubRepos(accessToken, githubUser.Repos_Url));
    //
    //     return Ok(new { accessToken });
    // }

    [HttpGet("auth/google")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginGoogleUser([FromQuery] string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
            return BadRequest("Access token is missing.");

        using var httpClient = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v1/userinfo");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var userResponse = await httpClient.SendAsync(request);

        var userResponseBody = await userResponse.Content.ReadAsStringAsync();

        var googleUser = JsonConvert.DeserializeObject<GoogleUser>(userResponseBody)
                         ?? throw new SerializationException();

        var existingUser = await userManager.FindByEmailAsync(googleUser.Email);

        var imageUrl =
            await s3BucketService.UploadImageFromUrlToS3Async(googleUser.Picture, $"profile-images/{googleUser.Email}");

        return existingUser is null
            ? await Register(new RegisterUserModel(FirstName: googleUser.Name, LastName: googleUser.Family_Name ,Email: googleUser.Email,
                ImageUrl: imageUrl, Password: "",
                RegistrationType: googleUser.Provider))
            : await LogIn(new UserLoginModel(Email: googleUser.Email, Password: "",
                LoginType: googleUser.Provider));
    }


    // [HttpPost("balance/fill")]
    // [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    // public async Task<IActionResult> FillUpUserBalance([FromBody] CreateTransactionCommand createTransactionCommand)
    // {
    //     try
    //     {
    //         await sender.Send(createTransactionCommand);
    //         return Ok("Transaction successful!");
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogError("{Date}: Payment error: {errorMessage}", DateTime.Now, ex.Message);
    //         return BadRequest(new { message = "Error during payment" });
    //     }
    //
    // }
    
    [AllowAnonymous]
    [HttpPost("password-recovery-request")]
    public async Task<IActionResult> PasswordRecovery([FromBody] PasswordRecoveryRequestModel passwordResetRequestModel)
    {
        try
        {
            await sender.Send(passwordResetRequestModel);

            return Ok(new { message = "If your email is registered, you will receive a password recovery link." });
        }
        catch (UserNotFoundException ex)
        {
            logger.LogError("{Date}: Error while requesting password recovery: {errorMessage}", DateTime.Now,
                ex.Message);
            return BadRequest(new { message = "User was not found with given email." });
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Error while requesting password recovery: {errorMessage}", DateTime.Now,
                ex.Message);
            return BadRequest("Error while requesting password recovery");
        }
    }
    
    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetModel passwordResetModel)
    {
        var result =
            await sender.Send(passwordResetModel);

        if (result.Succeeded)
        {
            return Ok("Password reset!");
        }

        return BadRequest("could not reset the password :(");
    }
}