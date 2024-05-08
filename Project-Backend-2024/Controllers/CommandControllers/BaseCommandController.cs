using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Controllers.CommandControllers;


[ApiController]
[Route("commands/[controller]")]
public abstract class BaseCommandController<TModel, TCommand> : Controller
    where TModel : class, IEntityModel
    where TCommand : ICommandModel<TModel>
{
    protected TCommand _commandService;

    public BaseCommandController(TCommand commandService) => _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

    [HttpPost("insert")]
    public virtual async Task<IActionResult> Insert([FromBody] TModel model)
    {
        try
        {
            await _commandService.Insert(model);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"Database failed: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest($"Invalid operation: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Insert operation failed: {ex.Message}");
        }

        return Ok("Entity inserted successfully!");
    }

    [HttpDelete("delete/{id:int}")]
    public virtual async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _commandService.Delete(id);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"Database failed: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest($"Invalid operation: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Delete operation failed: {ex.Message}");
        }
        return Ok("Entity deleted successfully!");
    }


    [HttpPut("update/{id:int}")]
    public virtual async Task<IActionResult> Update([FromRoute] int id, [FromBody] TModel model)
    {
        try
        {
            await _commandService.Update(id, model);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest($"Database failed: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest($"Invalid operation: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Update operation failed: {ex.Message}");
        }

        return Ok("Entity updated successfully");
    }
}




