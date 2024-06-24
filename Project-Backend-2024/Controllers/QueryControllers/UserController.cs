using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Caching;
using Project_Backend_2024.Services.Interfaces.Queries;


namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("queries/[controller]")]
public class UserController(IUserQueryService userQueryService, 
    IMemoryCache cache,  CachingService cachingService, CacheConfiguration cacheConfig) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpGet("admin/getByEmailAlike/{email}")]
    public async Task<IActionResult> GetByEmailAlike(string email)
    {
        var userModels = await userQueryService.GetByEmailAlikeAsync(email);

        if (userModels is null)
        {
            return NotFound("Could not find user with given email");
        }
        return Ok(userModels.ToList());
    }

    [Authorize( AuthenticationSchemes = "Cookies",Policy="AdminOrUser")]
    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        try
        {
            var watch = Stopwatch.StartNew();
    
            if (cache.TryGetValue(cacheConfig.CacheKey, out List<UserQueryModel>? users))
            {
                watch.Stop();
                Console.WriteLine($"Users found in cache! Time: {watch.ElapsedMilliseconds} ms");
                return users is null ? Ok("No users found") : Ok(users);
            }

            var cacheEntry = cachingService.BuildCacheOptions();
            var userModels = userQueryService.GetAll().ToList();
            cache.Set(cacheConfig.CacheKey, userModels, cacheEntry);

            watch.Stop();
            Console.WriteLine($"Users not found in cache, fetching from DB. Time: {watch.ElapsedMilliseconds} ms");
            return Ok(userModels);
        }
        catch (Exception)
        {
            return Unauthorized("You are not authorized");
        }
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("admin/getAll")]
    public IActionResult AdminGetAll()
    {
        try
        {
            var userModels = userQueryService.AdminGetAll().ToList();
            return Ok(userModels);
        }
        catch (Exception)
        {
            return Unauthorized("you are not authorized, fuck off");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("getByEmail/{email}")]
    public async Task<IActionResult> GetBy([FromRoute] string email)
    {
        try
        {
            var userModel = await userQueryService.GetByAsync
                (e => e.Email == email);
            
            return Ok(userModel);
        }
        catch (Exception)
        {
            return Unauthorized("you are not authorized, fuck off");
        }
    }

}