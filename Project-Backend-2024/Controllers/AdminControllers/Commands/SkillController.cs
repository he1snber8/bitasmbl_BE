using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Services.CommandServices.Skills.Create;
using Project_Backend_2024.Services.CommandServices.Skills.Delete;

namespace Project_Backend_2024.Controllers.AdminControllers.Commands;

[ApiController]
[Route("admin/[controller]")]
public class SkillController(ISender sender) : Controller
{

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> CreateSkill([FromQuery] string name)
    {
        try
        {
            await sender.Send(new CreateSkillCommand(name));

            return Ok("Skill added successfully!");
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpDelete]
    public async Task<IActionResult> DeleteSkill([FromQuery] string name)
    {
        try
        {
            await sender.Send(new DeleteSkillCommand(name));

            return Ok("Skill deleted successfully!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest("Error: " + ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("Error bro!");
        }
    }

}