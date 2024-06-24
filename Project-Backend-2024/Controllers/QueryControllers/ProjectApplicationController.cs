using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.BasicGetModels;
using Project_Backend_2024.Facade.BasicInsertModels;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Caching;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

public class ProjectApplicationController : BaseQueryController
    <ProjectApplication, ProjectApplicationModel, ProjectApplicationBasicGetModel, IProjectApplicationQueryService>
{
    public ProjectApplicationController(IProjectApplicationQueryService queryService, IMapper mapper, 
        CachingService cachingService, CacheConfiguration cacheConfiguration,
        IMemoryCache cache) : base(queryService, mapper, cachingService, cacheConfiguration, cache) { }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("get-my-applications")]
    public async Task<IActionResult> GetAllApplications()
    {
        try
        {
            var applicationModels = await GetAll();

            if (applicationModels is null || applicationModels.Count == 0) return Ok("You have no applications yet");
          
            return Ok(applicationModels);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-application/{id:int}")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        try
        {
            var applicationModel = await GetById(id);

            return applicationModel is null ? Ok("No application found with given Id") : Ok(applicationModel);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }
}