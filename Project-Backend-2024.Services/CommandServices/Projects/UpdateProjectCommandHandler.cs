using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Projects;

public class UpdateProjectCommandHandler(IHttpContextAccessor httpContextAccessor,IMapper mapper, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateProjectModel,GetUserProjectModel>
{
    public async Task<GetUserProjectModel> Handle(UpdateProjectModel request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
                     ?? throw new UserNotFoundException();
        
        var existingProject = await unitOfWork.ProjectRepository.Set(p => p.Id == request.Id && p.PrincipalId == userId).
            FirstOrDefaultAsync(cancellationToken)
                         ?? throw new ProjectNotFoundException();

         mapper.Map(request, existingProject);

         unitOfWork.ProjectRepository.Update(existingProject);
         
         await unitOfWork.SaveChangesAsync();

         var projectModel = mapper.Map<GetUserProjectModel>(existingProject);

         return projectModel;
    }
}