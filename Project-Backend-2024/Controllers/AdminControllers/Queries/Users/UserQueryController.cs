using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.QueryServices.Users.Admin.Get;
using Project_Backend_2024.Services.QueryServices.Users.Admin.List;

namespace Project_Backend_2024.Controllers.AdminControllers.Queries.Users;

[ApiController]
[Route("queries/admin/users")]
public class UserQueryController(ISender sender) : Controller
{
        
    [Authorize(AuthenticationSchemes = "Cookies",Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> ListAllUsers()
    {
        try
        {
            var models = await sender.Send(new ListAllUsersQuery());
            
            return Ok(models);
        }
        catch (Exception)
        {
            return Unauthorized("you are not authorized, fuck off");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        try
        {
            var model = await sender.Send(new GetUserByEmailQuery(email));

            return Ok(model);
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.ToString());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

}