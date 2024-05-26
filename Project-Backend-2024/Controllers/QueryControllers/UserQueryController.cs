using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

public class UserQueryController /*: BaseQueryController<User, UserModel, IUserQueryService>*/
{
    //public UserQueryController(IUserQueryService queryModel) : base(queryModel) { }
 
    //[HttpGet("get/{orderby}")]
    //[Authorize(Policy = "UserOnly")]
    //public ActionResult<IOrderedQueryable<UserModel>> GetQuery([FromRoute]string orderby)
    //{
    //    try
    //    {
    //        List<UserModel> result = orderby.ToLower() switch
    //        {
    //            "id" => _queryModel.Set().OrderBy(m => m.Id).ToList(),
    //            "username" => _queryModel.Set().OrderBy(m => m.Username).ToList(),
    //            "lastlogin" => _queryModel.Set().OrderByDescending(m => m.LastLogin).ToList(),
    //            _ => throw new ArgumentException("Invalid orderby parameter", nameof(orderby))
    //        };

    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
}