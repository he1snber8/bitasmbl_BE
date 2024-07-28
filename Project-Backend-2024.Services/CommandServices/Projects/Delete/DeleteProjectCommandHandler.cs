using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.CommandServices.Projects.Create;

namespace Project_Backend_2024.Services.CommandServices.Projects.Delete;

public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork, 
    IHttpContextAccessor httpContextAccessor, ILogger<CreateProjectCommandHandler> logger) 
    : IRequestHandler<DeleteProjectCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var httpUser = httpContextAccessor.HttpContext.User;

        if (httpUser.Identity is null)
            throw new ArgumentNullException(nameof(httpUser), "Could not retrieve user");

        var userId = httpUser.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
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