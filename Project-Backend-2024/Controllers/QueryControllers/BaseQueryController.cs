using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    [HttpGet("get")]
    public virtual async Task<List<TEntityModel>> GetAll()
    {
       return await _queryModel.GetAll();
    }

    [HttpGet("get/{id:int}")]
    public virtual TEntityModel GetById([FromRoute] int id)
    {
        return _queryModel.GetById(id);
    }

}
