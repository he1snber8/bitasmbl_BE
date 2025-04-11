using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade;

namespace Project_Backend_2024.Controllers.AdminControllers.Commands;

[ApiController]
[Route("admin/[controller]")]
public class RequirementController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> AddRequirement([FromBody] AddRequirementModel addRequirementModel)
    {
        try
        {
            await sender.Send(addRequirementModel);

            return Ok("Requirement added successfully!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error bro!: {ex.Message}");
        }
    }
}