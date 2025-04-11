using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Services.QueryServices.Projects.Admin.List;
using Project_Backend_2024.Services.QueryServices.Projects.List;

namespace Project_Backend_2024.Controllers.AdminControllers.Queries;

[ApiController]
[Route("[controller]/admin")]
public class ProjectsController(ISender sender) : Controller
{
    // [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    // [HttpGet]
    // public async Task<IActionResult> ListAllProjects()
    // {
    //     try
    //     {
    //         var role = User.FindFirst(ClaimTypes.Role)?.Value
    //                    ?? throw new ArgumentNullException();
    //         
    //         if (role is "Admin")
    //             return Ok(await sender.Send(new ListAllProjectsQueryAdmin()));
    //         var models = await sender.Send(new ListAllProjectsQuery());
    //
    //         return Ok(models);
    //     }
    //     catch (Exception)
    //     {
    //         return Unauthorized("you are not authorized");
    //     }
    // }
}