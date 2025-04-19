// using AutoMapper;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.AdminModels;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Facade.FetchModels;
// using Project_Backend_2024.Facade.GetModels;
// using Project_Backend_2024.Facade.Interfaces;
//
// namespace Project_Backend_2024.Services.CommandServices.ProjectApplications;
//
// public class UpdateProjectApplicationCommandHandler(
//     IUnitOfWork unitOfWork,
//     IMapper mapper,
//     UserManager<User> userManager,
//     IHttpContextAccessor httpContextAccessor,
//     IProjectRepository projectRepository)
//     : IRequestHandler<UpdateApplicationModel, GetProjectApplicationModel>
// {
//     public async Task<GetProjectApplicationModel> Handle(UpdateApplicationModel request,
//         CancellationToken cancellationToken)
//     {
//         var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
//                      ?? throw new UserNotFoundException();
//
//         var projectApplication = await projectRepository.Set(pr => pr.PrincipalId == userId)
//                                      .SelectMany(p => p.ProjectApplications)
//                                      .FirstOrDefaultAsync(pa => pa.Id == request.Id,
//                                          cancellationToken: cancellationToken) ??
//                                  throw new ArgumentException("Application does not exist");
//
//         if (projectApplication.PrincipalId != userId) throw new UnauthorizedAccessException("Request denied");
//
//         var appliedProject = userManager.FindByIdAsync(userId).Result?.AppliedProjects
//             .FirstOrDefault(u => u.UserId == userId && u.ProjectId == projectApplication.ProjectId);
//
//         
//         mapper.Map(request, projectApplication);
//         mapper.Map(request, appliedProject);
//
//         await unitOfWork.SaveChangesAsync();
//
//         var projectApplicationModel = mapper.Map<GetProjectApplicationModel>(projectApplication);
//
//         return projectApplicationModel;
//     }
// }