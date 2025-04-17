using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;
using Project_Backend_2024.Services.QueryServices.Projects.Get;
using Project_Backend_2024.Services.QueryServices.Projects.List;

namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController(ISender sender, ILogger<ProjectsController> logger) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProjects([FromQuery] string? projectName)
    {
        try
        {
            logger.LogInformation("Attempting to retrieve user's projects.");

            var projectModels = projectName is not null
                ? await sender.Send(new ListMyProjectsQuery(projectName))
                : await sender.Send(new ListMyProjectsQuery());

            if (projectModels is null || projectModels.Count == 0)
            {
                logger.LogInformation("No projects found for the user.");
                return Ok("No projects found. Start by creating your first project!");
            }

            logger.LogInformation("Successfully retrieved user's projects.");
            return Ok(projectModels);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve user's projects.");
            return BadRequest("An error occurred while fetching your projects. Please try again later.");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ListAllProjects([FromQuery] int page = 1, [FromQuery] int pageSize = 8)
    {
        try
        {
            logger.LogInformation("Fetching all available projects.");

            var projects = await sender.Send(new ListAllProjectsQuery(page,pageSize));

            if (projects?.Count == 0)
            {
                logger.LogInformation("No projects available in the system.");
                return Ok("Currently, there are no projects available. Check back later!");
            }

            logger.LogInformation("Successfully retrieved all projects.");
            return Ok(projects);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve all projects.");
            return Unauthorized("You are not authorized to view these projects.");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("applications/project/{id:int}")]
    public async Task<IActionResult> GetMyProjectApplications(int? id)
    {
        try
        {
            var applicationModels = id is not null
                ? await sender.Send(new GetMyProjectApplicationsQuery(id))
                : await sender.Send(new GetMyProjectApplicationsQuery(0));
    
            return applicationModels.Count == 0
                ? Ok("No applications found for your projects.")
                : Ok(applicationModels);
        }
        catch (ProjectNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving project applications.");
            return BadRequest("Unable to fetch project applications. Please try again later.");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMyProjectApplication(int id)
    {
        try
        {
            var projectModel = await sender.Send(new GetProjectByIdQuery(id));
            
            return Ok(projectModel);
        }
        catch (ProjectNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving project applications.");
            return BadRequest("Unable to fetch project applications. Please try again later.");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("applied")]
    public async Task<IActionResult> GetMyAppliedProjects()
    {
        try
        {
            logger.LogInformation("Fetching projects the user has applied to.");

            var appliedProjects = await sender.Send(new GetMyAppliedProjectsQuery());

            if (appliedProjects.Count == 0)
            {
                logger.LogInformation("No applied projects found for the user.");
                return Ok("You haven't applied to any projects yet.");
            }

            logger.LogInformation("Successfully retrieved applied projects.");
            return Ok(appliedProjects);
        }
        catch (ProjectNotFoundException ex)
        {
            logger.LogWarning(ex, "No applied projects found for the user.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving applied projects.");
            return BadRequest("Unable to fetch your applied projects. Please try again later.");
        }
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetProjectCategories()
    {
        try
        {
            var categories = await sender.Send(new GetCategoriesModel());
            return Ok(categories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("requirements")]
    public async Task<IActionResult> GetProjectRequirements()
    {
        try
        {
            var requirements = await sender.Send(new GetRequirementsModel());
            return Ok(requirements);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("requirement/tests/")]
    public async Task<IActionResult> GetProjectRequirementTests([FromQuery] string requirementNames)
    {
        try
        {
            var requirementTests = await sender.Send(new GetRequirementTest(requirementNames));
            
            return Ok(requirementTests);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}