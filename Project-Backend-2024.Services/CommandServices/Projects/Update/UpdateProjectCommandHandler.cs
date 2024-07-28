using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Projects.Update;

public class UpdateProjectCommandHandler(IHttpContextAccessor httpContextAccessor,IMapper mapper, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateProjectCommand,Unit>
{
    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var httpUser = httpContextAccessor.HttpContext.User;

        if (httpUser.Identity is null)
            throw new ArgumentNullException(nameof(httpUser), "Could not retrieve user");
        
        var userId = httpUser.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
                     ?? throw new UserNotFoundException();
        
        var existingProject = await unitOfWork.ProjectRepository.Set(p => p.Id == request.Id && p.PrincipalId == userId).
            FirstOrDefaultAsync(cancellationToken)
                         ?? throw new ProjectNotFoundException();

         mapper.Map(request, existingProject);

         unitOfWork.ProjectRepository.Update(existingProject);
         
         await unitOfWork.SaveChangesAsync();

         return Unit.Value;
    }
}