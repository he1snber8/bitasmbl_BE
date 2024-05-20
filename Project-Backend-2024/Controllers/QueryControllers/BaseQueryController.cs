using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("queries/[controller]")]
public abstract class BaseQueryController<TEntity, TEntityModel, TQueryModel> : Controller
    where TEntity : class, IEntity
    where TEntityModel : class, IEntityModel
    where TQueryModel : IQueryModel<TEntity, TEntityModel>
{
    protected TQueryModel _queryModel;

    public BaseQueryController(TQueryModel queryModel)
    {
        _queryModel = queryModel;   
    }

    [Authorize]
    [HttpGet("get")]
    public virtual async Task<ActionResult<List<TEntityModel>>> GetAll()
    {
        try
        {
            return Ok(await _queryModel.GetAll());
        }
        catch
        {
            return Unauthorized("You are an unauthorized");
        }
       
    }

    [HttpGet("get/{id:int}")]
    public virtual async Task<ActionResult<TEntityModel>> GetById([FromRoute] int id)
    {
        return Ok( await _queryModel.GetByIdAsync(id));
    }

}
