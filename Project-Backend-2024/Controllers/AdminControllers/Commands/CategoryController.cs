using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade;

namespace Project_Backend_2024.Controllers.AdminControllers.Commands;

[ApiController]
[Route("admin/[controller]")]
public class CategoryController(ISender sender) : Controller
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryModel addCategoryModel)
    {
        try
        {
            await sender.Send(addCategoryModel);

            return Ok("Category added successfully!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error bro!: {ex.Message}");
        }
    }
}