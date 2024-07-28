using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Controllers.AdminControllers.Commands;

[ApiController]
[Route("admin/[controller]")]
public class UserController(ISender sender,UserManager<User> userManager) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost("lock")]
    public async Task<IActionResult> LockoutUser([FromQuery] string userId, DateTime lockoutEnd)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null) return BadRequest("Failed to lock out user.");
        
        var result = await userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        
        if (result.Succeeded)
        {
            return Ok("User locked out until " + lockoutEnd);
        }
        return BadRequest("Failed to lock out user.");
    }
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost("unlock")]
    public async Task<IActionResult> UnlockUser([FromQuery]string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        
        if (user == null) return BadRequest("Failed to unlock user.");
        
        var result = await userManager.SetLockoutEndDateAsync(user, null);
        
        if (result.Succeeded)
        {
            return Ok("User unlocked.");
        }
        return BadRequest("Failed to unlock user.");
    }
    
}