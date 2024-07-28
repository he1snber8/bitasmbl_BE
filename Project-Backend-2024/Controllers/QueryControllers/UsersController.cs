using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Services.QueryServices.Users.Get;
using Project_Backend_2024.Services.QueryServices.Users.List;


namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("[controller]")]
public class UsersController(UserManager<User> userManager,ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy="AdminOrUser")]
    [HttpGet("")]
    public async Task<ActionResult<List<GetUserModel>>> ListAllUsers()
    {
        try
        { 
            return Ok(await sender.Send(new ListAllUsersQuery()));
        }
        catch (Exception)
        {
            return Unauthorized("You are not authorized");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        try
        {
            var userModel = await sender.Send(new GetUserByEmailQuery(email));
            
            return Ok(userModel);
        }
        catch (Exception)
        {
            return Unauthorized("you are not authorized, fuck off");
        }
    }

    [HttpGet("confirm")]
    public async Task<IActionResult> ConfirmEmail([FromQuery]string? userId,[FromQuery]string? token)
    {
        if (userId is null || token is null)
            return BadRequest("Invalid email confirmation request.");

        var user = await userManager.FindByIdAsync(userId);
            
        if (user is null)
            return BadRequest("User not found.");
        
        var result = await userManager.ConfirmEmailAsync(user, token);
        
        if (result.Succeeded)
            return Ok("Email confirmed successfully.");
        
        return BadRequest("Email confirmation failed.");
    }

}