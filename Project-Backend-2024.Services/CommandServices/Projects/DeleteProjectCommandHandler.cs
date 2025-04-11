using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Projects;

public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork, 
    IHttpContextAccessor httpContextAccessor, ILogger<CreateProjectCommand> logger) 
    : IRequestHandler<DeleteProjectModel, Unit>
{
    public async Task<Unit> Handle(DeleteProjectModel request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
                     ?? throw new UserNotFoundException();

        var project = await unitOfWork.ProjectRepository.Set(p => p.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null) throw new ProjectNotFoundException(request.Id);
        
        if (project.PrincipalId != userId)
            throw new UnauthorizedAccessException("You are not authorized to delete this project.");

        project.IsDeleted = true;
        project.Status = ProjectStatus.Inactive;

        unitOfWork.ProjectRepository.Update(project);

        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("[{Date}] User with an ID {userId} deleted project with ID {projectId} successfully.",
            DateTime.Now, userId, request.Id);

        return Unit.Value;
    }
}