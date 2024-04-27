using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Controllers;

[ApiController]
[Route("api/commands/users")]
public class UserCommandController : BaseController<UserModel, IUserCommand>
{
    public UserCommandController(IUserCommand command) : base(command) { }

    public override async Task<IActionResult> Insert([FromBody] UserModel model) => await base.Insert(model);

    public override async Task<IActionResult> Delete(int id) => await base.Delete(id);

    public override async Task<IActionResult> Update(int id, UserModel model) => await base.Update(id, model);
}




