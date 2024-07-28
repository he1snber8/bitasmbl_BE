using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Services.QueryServices.Projects.Admin.List;

namespace Project_Backend_2024.Controllers.AdminControllers.Queries;

[ApiController]
[Route("[controller]/admin")]
public class ProjectsController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<List<ProjectModel>>> ListAllProjects()
    {
        try
        {
            var models = await sender.Send(new ListAllProjectsQuery());
            
            return Ok(models);
        }
        catch (Exception)
        {
            return Unauthorized("you are not authorized, fuck off");
        }
    }
}