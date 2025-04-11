using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade;

namespace Project_Backend_2024.Controllers.AdminControllers.Commands;

[ApiController]
[Route("admin/[controller]")]
public class SkillController(ISender sender) : Controller
{

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> CreateSkill([FromBody] AddSkillModel applySkillModel)
    {
        try
        {
            await sender.Send(applySkillModel);

            return Ok("Skill added successfully!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error bro!: {ex.Message}" );
        }
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpDelete]
    public async Task<IActionResult> DeleteSkill([FromBody] RemoveSkillModel removeSkillModel)
    {
        try
        {
            await sender.Send(removeSkillModel);

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