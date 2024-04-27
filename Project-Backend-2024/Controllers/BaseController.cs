using Microsoft.AspNetCore.Mvc;

using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Controllers;

public abstract class BaseController<TModel, TCommand> : Controller
    where TModel : class, IBasicModel
    where TCommand : ICommandModel<TModel>
{
    protected TCommand _command;

    public BaseController(TCommand command) => _command = command ?? throw new ArgumentNullException(nameof(command));

    [HttpPost("insert")]
    public virtual async Task<IActionResult> Insert([FromBody] TModel model)
    {
        try
        {
            await _command.Insert(model);
        }
        catch (Exception ex)
        {
            return BadRequest($"operation failed, reason: {ex.Message}");
        }

        return Ok("Entity inserted successfully!");
    }

    [HttpDelete("delete")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _command.Delete(id);
        }
        catch (Exception ex)
        {
            return BadRequest($"operation failed, reason: {ex.Message}");
        }

        return Ok("Entity deleted successfully!");
    }


    [HttpPut("update")]
    public virtual async Task<IActionResult> Update(int id, TModel model)
    {
        try
        {
           await _command.Update(id, model);
        }
        catch (Exception ex)
        {
            return BadRequest($"operation failed, reason: {ex.Message}");
        }

        return Ok("Entity updated successfully");
    }
}




