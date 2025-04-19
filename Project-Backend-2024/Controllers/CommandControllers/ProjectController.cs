using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController(
    ISender sender,
    ILogger<ProjectsController> logger,
    IHubContext<ProjectsHub> projectsHub) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost]
    public async Task<IActionResult> CreateCommand([FromBody] CreateProjectModel projectCommand)
    {
        try
        {

            var projectId = await sender.Send(projectCommand);

            logger.LogInformation("{Date}: Project created successfully.", DateTime.Now);
            return Ok(new { message = "Project created successfully!", projectId });
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InsufficientFundsException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EmptyValueException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ProjectCategoryNotSelectedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ProjectRequirementNotSelectedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ProjectMediaNotIncludedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityAlreadyExistsException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error: {Message}", DateTime.Now, ex.Message);
            return BadRequest(new { message = $"An error occurred while creating the project: {ex}" });
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPut]
    public async Task<IActionResult> UpdateCommand(UpdateProjectModel updateProjectModel)
    {
        try
        {
            var updatedProjectModel = await sender.Send(updateProjectModel);

            return Ok(updatedProjectModel);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EmptyValueException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ProjectNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error: {Message}", DateTime.Now, ex.Message);
            return StatusCode(500, "An error occurred while updating the project.");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCommand(int id)
    {
        try
        {
            await sender.Send(new DeleteProjectModel(id));

            logger.LogInformation("{Date}: Project deleted successfully.", DateTime.Now);
            return Ok("Project deleted!");
        }
        catch (ProjectNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning("{Date}: UnauthorizedAccessException: {Message}", DateTime.Now, ex.Message);
            return BadRequest(ex.Message);
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (EntityAlreadyExistsException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("{Date}: Unexpected error: {Message}", DateTime.Now, ex.Message);
            return StatusCode(500, "An error occurred while deleting the project.");
        }
    }

    // [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    // [HttpPost("apply")]
    // public async Task<IActionResult> ApplyProjectCommand([FromBody] ApplyToProjectModel applyToProjectModel)
    // {
    //     try
    //     {
    //         var appliedProject = await sender.Send(applyToProjectModel);
    //         // await projectsHub.Clients.All.SendAsync("ProjectApplied", appliedProject);
    //         await projectsHub.Clients.Group($"principal_{appliedProject.PrincipalId}")
    //             .SendAsync("ProjectApplied", appliedProject);
    //
    //         
    //         Console.WriteLine($"User applied and message sent to a group: principal_{appliedProject.PrincipalId}");
    //
    //         return Ok(appliedProject);
    //     }
    //     catch (UserNotFoundException ex)
    //     {
    //         return BadRequest(new { message = "User not found" });
    //     }
    //     catch (InvalidProjectStatusException ex)
    //     {
    //         return BadRequest(new { message = ex.Message });
    //     }
    //     catch (AlreadyAppliedException ex)
    //     {
    //         return BadRequest(new { message = ex.Message });
    //     }
    //     catch (ProjectMaxApplicationLimitException ex)
    //     {
    //         return BadRequest(new { message = ex.Message });
    //     }
    //     catch (InvalidOperationException ex)
    //     {
    //         return BadRequest(new { message = ex.Message });
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogError("{Date}: Unexpected error: {Message}", DateTime.Now, ex.Message);
    //         return StatusCode(500, new { message = ex.Message });
    //     }
    // }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("images/upload")]
    public async Task<IActionResult> UploadProjectImage([FromForm] UploadProjectImagesModel? uploadImagesModel)
    {
        if (uploadImagesModel is null) return Ok("No files were uploaded");
        try
        {
            await sender.Send(uploadImagesModel);

            logger.LogInformation("{Date}: Project updated successfully.", DateTime.Now);
            return Ok(new { message = "Image uploaded successfully!" });
        }
        catch (ProjectNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ProjectMediaNotIncludedException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogWarning("{Date}: Unexpected Error: {Message}", DateTime.Now, ex.Message);
            return BadRequest(new { message = "Something went wrong" });
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPut("applications/approve/{id:int}")]
    public async Task<IActionResult> ApproveApplication(int id)
    {
        try
        {
            var applicationModel = await sender.Send(new UpdateApplicationModel(id, "Approved"));

            return Ok(applicationModel);
        }
        catch
        {
            return BadRequest("Could not approve application");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPut("applications/reject/{id:int}")]
    public async Task<IActionResult> RejectApplication(int id)
    {
        try
        {
            var applicationModel = await sender.Send(new UpdateApplicationModel(id, "Rejected"));

            return Ok(applicationModel);
        }
        catch
        {
            return BadRequest("Could not reject application");
        }
    }
}