using System.Net.Http.Headers;
using System.Runtime.Serialization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Backend_2024.Controllers.CommandControllers;
using Project_Backend_2024.DTO.Github;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Services.QueryServices.Users.Get;
using Project_Backend_2024.Services.QueryServices.Users.List;
using User = Project_Backend_2024.DTO.User;


namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("[controller]")]
public class UsersController(UserManager<User> userManager, ISender sender, ILogger<UsersController> logger)
    : Controller
{
    // [Authorize(AuthenticationSchemes = "Cookies", Policy="AdminOrUser")]
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<GetUserModel>>> ListAllUsers()
    {
        try
        {
            logger.LogInformation("Fetching all users.");

            var users = await sender.Send(new ListAllUsersQuery());

            if (users is null || users.Count == 0)
            {
                logger.LogInformation("No users found.");
                return Ok("No users are currently registered.");
            }

            logger.LogInformation("Successfully retrieved all users.");
            return Ok(users);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching users.");
            return Unauthorized("You are not authorized to access user data.");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByUserById(string id)
    {
        try
        {
            var userModel = await sender.Send(new GetUserByIdQuery(id));

            if (userModel is null)
                return NotFound($"Could not find user");

            return Ok(userModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "An error occurred while fetching user");
            return Unauthorized("Error");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            logger.LogInformation("Fetching profile for the authenticated user.");
            var userModel = await sender.Send(new GetUserProfileQuery());

            logger.LogInformation("Successfully retrieved the user's profile.");
            return Ok(userModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching the user's profile.");
            return Unauthorized("You are not authorized to access your profile.");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("github/repos")]
    public async Task<IActionResult> GetGithubUserRepos([FromQuery] string accessToken, [FromQuery] string username)
    {
        using var httpClient = new HttpClient();

        // var accessToken = await sender.Send(new GetGithubAccessToken(code));

        if (string.IsNullOrEmpty(accessToken))
            return BadRequest("Access token missing in response.");

        var reposUrl = $"https://api.github.com/users/{username}/repos";

        var request = new HttpRequestMessage(HttpMethod.Get, reposUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.UserAgent.ParseAdd("Bitasmbl");

        var repoResponse = await httpClient.SendAsync(request);

        var repoResponseBody = await repoResponse.Content.ReadAsStringAsync();

        var deserializedRepos = JsonConvert.DeserializeObject<List<GithubRepo>>(repoResponseBody)
                                ?? throw new SerializationException();

        return Ok(deserializedRepos);
    }


    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("github/repo/commits")]
    public async Task<IActionResult> GetGithubUserRepoCommits([FromQuery] string accessToken,
        [FromQuery] string username, [FromQuery] string repo, [FromQuery] string branch = "main")
    {
        using var httpClient = new HttpClient();

        // var accessToken = await sender.Send(new GetGithubAccessToken(code));

        if (string.IsNullOrEmpty(accessToken))
            return BadRequest("Access token missing in response.");

        var commitsUrl = $"https://api.github.com/repos/{username}/{repo}/commits?sha={branch}";

        var request = new HttpRequestMessage(HttpMethod.Get, commitsUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.UserAgent.ParseAdd("Bitasmbl");

        var commitsResponse = await httpClient.SendAsync(request);

        var repoResponseBody = await commitsResponse.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(repoResponseBody))
            return Ok(new { message = "empty commits" });

        if (repoResponseBody.TrimStart().StartsWith("{")) // If response is a JSON object
        {
            var deserializedCommit = JsonConvert.DeserializeObject<GithubCommit>(repoResponseBody)
                                      ?? throw new SerializationException();
            
            return Ok(deserializedCommit);
        }
        var deserializedCommits = JsonConvert.DeserializeObject<List<GithubCommit>>(repoResponseBody)
                                  ?? throw new SerializationException();

        return Ok(deserializedCommits);
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("github/repo/branches")]
    public async Task<IActionResult> GetGithubUserRepoBranches([FromQuery] string accessToken,
        [FromQuery] string username, [FromQuery] string repo)
    {
        using var httpClient = new HttpClient();

        // var accessToken = await sender.Send(new GetGithubAccessToken(code));

        if (string.IsNullOrEmpty(accessToken))
            return BadRequest("Access token missing in response.");

        var commitsUrl = $"https://api.github.com/repos/{username}/{repo}/branches";

        var request = new HttpRequestMessage(HttpMethod.Get, commitsUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.UserAgent.ParseAdd("Bitasmbl");

        var commitsResponse = await httpClient.SendAsync(request);

        var repoResponseBody = await commitsResponse.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(repoResponseBody))
            return Ok(new { message = "empty commits" });

        if (repoResponseBody.TrimStart().StartsWith("{")) // If response is a JSON object
        {
            var deserializedCommit = JsonConvert.DeserializeObject<GithubCommit>(repoResponseBody)
                                     ?? throw new SerializationException();
            
            return Ok(deserializedCommit);
        }
        var deserializedCommits = JsonConvert.DeserializeObject<List<GithubCommit>>(repoResponseBody)
                                  ?? throw new SerializationException();

        return Ok(deserializedCommits);
    }

    [HttpGet("confirm")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string? userId, [FromQuery] string? token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            logger.LogWarning("Invalid email confirmation request: Missing userId or token.");
            return BadRequest("Invalid email confirmation request.");
        }

        try
        {
            logger.LogInformation("Attempting to confirm email for userId: {UserId}", userId);

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                logger.LogWarning("User not found for userId: {UserId}", userId);
                return BadRequest("User not found.");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                logger.LogInformation("Email successfully confirmed for userId: {UserId}", userId);
                return Ok("Email confirmed successfully.");
            }

            logger.LogWarning("Email confirmation failed for userId: {UserId}", userId);
            return BadRequest("Email confirmation failed. Please ensure the link is correct and try again.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during email confirmation for userId: {UserId}", userId);
            return BadRequest("An error occurred while confirming the email. Please try again later.");
        }
    }
}