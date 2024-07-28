using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.QueryServices.Skills.Get;
using Project_Backend_2024.Services.QueryServices.Skills.List;

namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("[controller]")]
public class SkillsController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("get")]
    public async Task<IActionResult> GetSkillById([FromQuery] int id)
    {
        try
        {
            var skill = await sender.Send(new GetSkillByIdQuery(id));

            return Ok(skill);
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet]
    public async Task<IActionResult> GetAllSkills()
    {
        try
        {
            var skills = await sender.Send(new ListSkillsQuery());

            return Ok(skills);
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetMySkills()
    {
        try
        {
            var skills = await sender.Send(new ListUserSkillsQuery());
            
            return skills.Count == 0 ? Ok("You have no skills applied yet, do it now!") : Ok(skills);
            
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }

}