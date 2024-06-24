using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Facade.BasicInsertModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Commands;

namespace Project_Backend_2024.Controllers.CommandControllers;

public class ProjectApplicationController(
    IProjectApplicationCommandService applicationCommandService,
    IMapper mapper,
    IMemoryCache memoryCache)
    : BaseCommandController<ProjectApplicationModel, ProjectApplicationBasicInsertModel, IProjectApplicationCommandService>(
        applicationCommandService, mapper, memoryCache)
{
    private readonly IProjectApplicationCommandService _applicationCommandService = applicationCommandService;

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("apply")]
    public override async Task<IActionResult> Insert(ProjectApplicationBasicInsertModel basicInsertModel)
    {
        try
        {
            var application = Mapper.Map<ProjectApplicationModel>(basicInsertModel);
            
            await  _applicationCommandService.Insert(application);

            return Ok("Applied to project successfully!");
        }
        catch (UserNotFoundException)
        {
            return BadRequest("User not found");
        }
        catch (Exception)
        {
            return BadRequest("something went wrong");
        }
    }

    public override Task<IActionResult> Update(string filter, ProjectApplicationBasicInsertModel basicUpdateModel)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> Delete(int id)
    {
        throw new NotImplementedException();
    }
}