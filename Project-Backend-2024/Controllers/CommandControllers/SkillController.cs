using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Services.CommandServices.Skills;
using Project_Backend_2024.Services.CommandServices.Skills.Create;
using Project_Backend_2024.Services.CommandServices.UserSkills.Create;
using Project_Backend_2024.Services.CommandServices.UserSkills.Delete;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("[controller]")]
public class SkillController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("apply")]
    public async Task<IActionResult> CreateSkill([FromQuery] string name)
    {
        try
        {
            await sender.Send(new ApplySkillCommand(name));    

            return Ok("Skill applied!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpDelete("profile")]
    public async Task<IActionResult> RemoveSkill([FromQuery] string name)
    {
        try
        {
            await sender.Send(new RemoveSkillCommand(name));

            return Ok("Skill removed!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }
}


