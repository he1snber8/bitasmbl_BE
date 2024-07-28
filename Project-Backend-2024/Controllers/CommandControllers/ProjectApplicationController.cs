using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.CommandServices.ProjectApplications.Create;
using Project_Backend_2024.Services.CommandServices.ProjectApplications.Update;
using Project_Backend_2024.Services.CommandServices.Projects.Update;

namespace Project_Backend_2024.Controllers.CommandControllers;
[ApiController]
[Route("commands/[controller]")]
public class ProjectApplicationsController(ISender sender) : Controller
{
    // [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    // [HttpPost("apply")]
    // public  async Task<IActionResult> CreateCommand(CreateProjectApplicationCommand projectApplicationCommand)
    // {
    //     try
    //     {
    //         await sender.Send(projectApplicationCommand);
    //
    //         return Ok("Applied to project successfully!");
    //     }
    //     catch (UserNotFoundException)
    //     {
    //         return BadRequest("User not found");
    //     }
    //     catch (InvalidProjectStatusException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"something went wrong: {ex.Data}");
    //     }
    // }
    //
    // [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    // [HttpPost("update")]
    // public  async Task<IActionResult> UpdateCommand(UpdateProjectApplicationCommand projectApplicationCommand)
    // {
    //     try
    //     {
    //         await sender.Send(projectApplicationCommand);
    //
    //         return Ok("Updated project successfully!");
    //     }
    //     catch (UserNotFoundException)
    //     {
    //         return BadRequest("User not found");
    //     }
    //     catch (InvalidProjectStatusException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest($"something went wrong: {ex.Data}");
    //     }
    // }

}