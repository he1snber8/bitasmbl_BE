using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Facade.BasicInsertModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Commands;

namespace Project_Backend_2024.Controllers.CommandControllers;

public class ProjectController(
    IProjectCommandService commandService,
    IMapper mapper,
    ILogger<ProjectController> logger,
    IMemoryCache memoryCache)
    : BaseCommandController<ProjectModel, ProjectBasicAlterModel, IProjectCommandService>(commandService, mapper,
        memoryCache)
{

    [HttpPost("create-project")]
    public override async Task<IActionResult> Insert(ProjectBasicAlterModel basicAlterModel)
    {
        if (User.Identity is null)
            throw new UnauthorizedAccessException();
        
        try
        {
            var model = Mapper.Map<ProjectModel>(basicAlterModel);
            
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                         ?? throw new UserNotFoundException();

            model.PrincipalId = userId;
                
            await ApplicationCommandService.Insert(model);
            
            MemoryCache.Remove("SupaCache");
            Console.WriteLine("cache removed!");

            logger.LogInformation("[{Date}] User with an ID {userId} created project successfully.",
                DateTime.Now, userId);
            
            return Ok("Project created successfully!");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "User ID not found in claims.");
            return BadRequest("Could not retrieve user.");
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogError(ex, "Unauthorized acess");
            return BadRequest("Could not retrieve user.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating the project.");
            return StatusCode(500, "An error occurred while creating the project.");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPut("update/{name}")]
    public override async Task<IActionResult> Update(string name, ProjectBasicAlterModel updateModel)
    {
        try
        {
            if (User.Identity is null)
                throw new UnauthorizedAccessException();

            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                         ?? throw new UserNotFoundException();

            var project = Mapper.Map<ProjectModel>(updateModel);

            await ApplicationCommandService.Update(name, project, userId);

            return Ok("Project updated successfully");
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("You are not authorized to do that");
        }
        catch (UserNotFoundException)
        {
            return BadRequest("could not retrieve user");
        }
        catch (Exception)
        {
            return BadRequest("error");
        }
    }

    public override Task<IActionResult> Delete(int id)
    {
        throw new NotImplementedException();
    }
}