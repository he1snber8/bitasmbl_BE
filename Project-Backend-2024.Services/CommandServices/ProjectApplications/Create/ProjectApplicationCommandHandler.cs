using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;


namespace Project_Backend_2024.Services.CommandServices.ProjectApplications.Create;

public class ProjectApplicationCommandHandler(
    IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpHttpContextAccessor, UserManager<User> userManager)
    : IRequestHandler<ProjectApplicationCommand,Unit>
{
    public async Task<Unit> Handle(ProjectApplicationCommand request,  CancellationToken cancellationToken)
    {
        if (request.ProjectId <= 0) throw new ArgumentException("Invalid Project ID", nameof(request.ProjectId));

        var userId = httpHttpContextAccessor.HttpContext.User.Claims
                         .FirstOrDefault(c => c.Type == "Id")?.Value ??
                     throw new ArgumentException("user null");

        var project = await unitOfWork.ProjectRepository.Set(pr => pr.Id == request.ProjectId).FirstOrDefaultAsync(cancellationToken)
                      ?? throw new ArgumentException("Project does not exist");

        if (project.Status is not ProjectStatus.Active)
            throw new InvalidProjectStatusException(project.Status);
        
        if(userId == project.PrincipalId)
            throw new  ArgumentException("Cannot apply to own project");

        var existingApplication = await unitOfWork.ProjectApplicationRepository.Set(pa => 
            pa.ApplicantId == userId && pa.ProjectId == request.ProjectId).FirstOrDefaultAsync(cancellationToken);
        
        if (existingApplication != null)
        {
            throw new InvalidOperationException("User has already applied for this project");
        }

        var applicant = await userManager.FindByIdAsync(userId)
                        ?? throw new ArgumentException("Could not retrieve applicant");

        if (project.PrincipalId != null)
        {
            var principal = await userManager.FindByIdAsync(project.PrincipalId)
                            ?? throw new ArgumentException("Could not retrieve principal");

            if (string.IsNullOrEmpty(applicant.UserName) || string.IsNullOrEmpty(principal.UserName))
                throw new ArgumentException("Username is empty");
        }

        var application = mapper.Map<ProjectApplication>(request);
        
        application.ApplicantId = userId;
        application.PrincipalId = project.PrincipalId;
        application.Applicant = applicant.UserName;
        application.ProjectName = project.Name;

        unitOfWork.ProjectApplicationRepository.Insert(application);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}