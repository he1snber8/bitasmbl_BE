using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

public class UserController : BaseQueryController<User, UserModel, IUserQueryService>
{
    public UserController(IUserQueryService queryModel) : base(queryModel) { }
 
    [HttpGet("get/{orderby}")]
    public IActionResult GetQuery(string orderby)
    {
        IOrderedQueryable<UserModel> userModels;

        switch (orderby.ToLower())
        {
            case "id":
                userModels = _queryModel.Set().OrderBy(m => m.Id);
                break;
            case "username":
                userModels = _queryModel.Set().OrderBy(m => m.Username);
                break;
            case "lastlogin":
                userModels = _queryModel.Set().OrderByDescending(m => m.LastLogin);
                break;
            default:
                return BadRequest("Invalid orderBy parameter.");
        }

        return Ok(userModels.ToList());
    }
}