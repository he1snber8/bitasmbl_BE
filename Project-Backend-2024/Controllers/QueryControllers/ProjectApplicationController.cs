using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("queries/[controller]")]
public class ProjectApplicationController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("get-my-applications")]
    public async Task<IActionResult> GetAllApplications([FromQuery] string? name=null)
    {
        try
        {
            var applicationModels = name is not null ?
                await sender.Send(new GetMyProjectApplicationsQuery(name)) :
                await sender.Send(new GetMyProjectApplicationsQuery());

            return applicationModels.Count == 0 ? Ok("You have no applications yet") : Ok(applicationModels);
        }
        catch (ProjectNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }
}