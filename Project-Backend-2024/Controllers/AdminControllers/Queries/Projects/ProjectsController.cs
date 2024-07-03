using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.QueryServices.Projects.Admin.List;

namespace Project_Backend_2024.Controllers.AdminControllers.Queries.Projects;

[ApiController]
[Route("queries/admin/[controller]")]
public class ProjectsController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies",Policy = "AdminOnly")]
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