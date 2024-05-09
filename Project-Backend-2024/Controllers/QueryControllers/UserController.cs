using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

public class UserController : BaseQueryController<User, UserModel, IUserQueryService>
{
    public UserController(IUserQueryService queryModel) : base(queryModel) { }
 
    [HttpGet("get/{orderby}")]
    public ActionResult<IOrderedQueryable<UserModel>> GetQuery(string orderby)
    {
        try
        {
            switch (orderby.ToLower())
            {
                case "id":
                    return Ok(_queryModel.Set().OrderBy(m => m.Id).ToList());
                case "username":
                    return Ok(_queryModel.Set().OrderBy(m => m.Username).ToList());               
                case "lastlogin":
                    return Ok(_queryModel.Set().OrderByDescending(m => m.LastLogin).ToList());                  
                default:
                    return BadRequest("Invalid parameter.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}