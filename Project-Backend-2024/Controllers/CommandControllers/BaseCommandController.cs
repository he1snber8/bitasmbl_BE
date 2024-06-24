using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.BasicGetModels;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Controllers.CommandControllers;


[ApiController]
[Route("commands/[controller]")]
public abstract class BaseCommandController<TModel, TBasicModel ,TCommandService>
    (TCommandService applicationCommandService, IMapper mapper, IMemoryCache memoryCache) : Controller
    where TModel : class, IEntityModel
    where TBasicModel : IBasicInsertModel
    where TCommandService : ICommandModel<TModel>
{
    protected readonly TCommandService ApplicationCommandService = applicationCommandService;
    protected readonly IMapper Mapper = mapper;
    protected readonly IMemoryCache MemoryCache = memoryCache;
    
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    public abstract Task<IActionResult> Insert([FromBody] TBasicModel basicInsertModel);

    public abstract Task<IActionResult> Update(string filter, TBasicModel basicUpdateModel);

    [HttpDelete("delete/{id:int}")]
    public abstract Task<IActionResult> Delete([FromRoute] int id);

}




