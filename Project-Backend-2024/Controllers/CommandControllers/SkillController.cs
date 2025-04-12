using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade;

namespace Project_Backend_2024.Controllers.CommandControllers;

[ApiController]
[Route("[controller]")]
public class SkillController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("apply")]
    public async Task<IActionResult> CreateSkill([FromBody] ApplySkillModel applySkillModel)
    {
        try
        {
            await sender.Send(applySkillModel);    

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
    public async Task<IActionResult> RemoveSkill([FromBody] ApplySkillModel applySkillModel)
    {
        try
        {
            await sender.Send(applySkillModel);

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


