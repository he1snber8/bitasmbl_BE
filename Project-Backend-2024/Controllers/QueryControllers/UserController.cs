using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

public class UserController/* : BaseQueryController<User, UserModel, IUserQueryService>*/
{
    private readonly ILogger<UserController> _logger;

    public UserController(/*IUserQueryService queryModel,*/ ILogger<UserController> logger)/* : base(queryModel)*/
    {
        _logger = logger;
    }
 
    //[HttpGet("getby")]
    //[Authorize(Policy = "AdminOrUser")]
    //public async Task<ActionResult<IOrderedQueryable<UserModel>>> GetQuery([FromQuery]string orderby)
    //{
    //    try
    //    {
    //        List<UserModel> result = orderby.ToLower() switch
    //        {
    //            "id" => await _queryModel.Set().OrderBy(m => m.Id).ToListAsync(),
    //            "username" => await _queryModel.Set().OrderBy(m => m.Username).ToListAsync(),
    //            "lastlogin" => await _queryModel.Set().OrderByDescending(m => m.LastLogin).ToListAsync(),
    //            _ => throw new ArgumentException("Invalid orderby parameter", nameof(orderby))
    //        };

    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogInformation("{Date}: could not retrieve data: {errorMessage}",
    //        DateTime.Now, ex.Message);
    //        return BadRequest();
    //    }
    //}
}