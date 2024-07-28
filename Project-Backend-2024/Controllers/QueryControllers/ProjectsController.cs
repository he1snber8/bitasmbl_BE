using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;
using Project_Backend_2024.Services.QueryServices.Projects.List;

namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProjects()
    {
        try
        {
            var projectModels = await sender.Send(new ListMyProjectsQuery());

            return projectModels is null || 
                   projectModels.Count == 0 
                ? Ok("You have no projects, create one!") 
                : Ok(projectModels);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }
    
    // [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [AllowAnonymous]
    [HttpGet]
    public  async Task<IActionResult> GetAllProjects()
    {
        try
        {
            var projectModels = await sender.Send(new ListAllProjectsQuery());
    
            return projectModels is null ? Ok("Projects not found") : Ok(projectModels);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("applications")]
    public async Task<IActionResult> GetMyProjectApplications([FromQuery]string? project=null)
    {
        try
        {
            var applicationModels = project is not null ?
                await sender.Send(new GetMyProjectApplicationsQuery(project)) :
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
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("applied")]
    public async Task<IActionResult> GetMyAppliedProjects()
    {
        try
        {
            var appliedProjects = await sender.Send(new GetMyAppliedProjectsQuery());

            return appliedProjects.Count == 0 ? Ok("You have not applied to any projects yet") : Ok(appliedProjects);
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