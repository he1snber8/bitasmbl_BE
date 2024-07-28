using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.CommandServices.ProjectApplications.Create;
using Project_Backend_2024.Services.CommandServices.ProjectApplications.Update;
using Project_Backend_2024.Services.CommandServices.Projects.Create;
using Project_Backend_2024.Services.CommandServices.Projects.Delete;
using Project_Backend_2024.Services.CommandServices.Projects.Update;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("[controller]")]
public class ProjectController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost]
    public async Task<IActionResult> CreateCommand([FromQuery] string name, [FromQuery] string description)
    {
        try
        {
            await sender.Send(new CreateProjectCommand(name, description));

            return Ok("Project created successfully!");
        }
        catch (ArgumentNullException ex) {return BadRequest(ex.Message);}

        catch (EmptyValueException ex) { return BadRequest(ex.Message);}

        catch (UserNotFoundException ex) {return BadRequest(ex.Message);}

        catch (EntityAlreadyExistsException ex) {return BadRequest(ex.Message);}

        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the project.");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPut]
    public async Task<IActionResult> UpdateCommand(int id, string? name, string? description, string? status)
    {
        try
        {
            await sender.Send(new UpdateProjectCommand(id,name,description,status));

            return Ok("Project updated successfully!");
        }
        catch (ArgumentNullException ex) { return BadRequest(ex.Message); } 
        
        catch (EmptyValueException ex) { return BadRequest(ex.Message); }

        catch (UserNotFoundException ex) { return BadRequest(ex.Message); }
        
        catch (ProjectNotFoundException ex) { return BadRequest(ex.Message); }
        
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the project.");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCommand(DeleteProjectCommand deleteProjectCommand)
    {
        try
        {
            await sender.Send(deleteProjectCommand);

            return Ok("Project deleted!");
        }
        catch (ProjectNotFoundException ex) {return BadRequest(ex.Message);}

        catch (UnauthorizedAccessException ex) { return BadRequest(ex.Message);}

        catch (UserNotFoundException ex) {return BadRequest(ex.Message);}

        catch (EntityAlreadyExistsException ex) {return BadRequest(ex.Message);}

        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the project.");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("apply")]
    public  async Task<IActionResult> CreateCommand(ProjectApplicationCommand projectApplicationCommand)
    {
        try
        {
            await sender.Send(projectApplicationCommand);

            return Ok("Applied to project successfully!");
        }
        catch (UserNotFoundException)
        {
            return BadRequest("User not found");
        }
        catch (InvalidProjectStatusException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"something went wrong: {ex.Data}");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPut("applications/update")]
    public  async Task<IActionResult> UpdateCommand(UpdateProjectApplicationCommand projectApplicationCommand)
    {
        try
        {
            await sender.Send(projectApplicationCommand);

            return Ok("Updated project successfully!");
        }
        catch (UserNotFoundException)
        {
            return BadRequest("User not found");
        }
        catch (InvalidProjectStatusException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"something went wrong: {ex.Data}");
        }
    }
}


