using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
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

public class ProjectController :  BaseQueryController
<Project, ProjectModel, ProjectBasicGetModel, IProjectQueryService>
{
    public ProjectController(
        IProjectQueryService queryService, IMapper mapper,
        CachingService cachingService, CacheConfiguration cacheConfiguration, IMemoryCache cache)
        : base(queryService, mapper, cachingService, cacheConfiguration, cache) {}
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("get-my-projects")]
    public async Task<IActionResult> GetAllProjects()
    {
        try
        {
            var projectModels = await GetAll();

            return projectModels is null || projectModels.Count == 0 
                ? Ok("You have no projects, create one!") 
                : Ok(projectModels);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("get-project/{id:int}")]
    public  async Task<IActionResult> GetProjectById(int id)
    {
        try
        {
            var projectModel = await GetById(id);

            return projectModel is null ? Ok("No project found with given Id") : Ok(projectModel);
        }
        catch (Exception)
        {
            return BadRequest("Something went wrong");
        }
    }

}