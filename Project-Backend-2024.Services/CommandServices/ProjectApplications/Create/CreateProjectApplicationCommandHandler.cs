using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Queries;


namespace Project_Backend_2024.Services.CommandServices.ProjectApplications.Create;

public class CreateProjectApplicationCommandHandler(
    IUnitOfWork unitOfWork, IMapper mapper, IProjectApplicationRepository applicationRepository,
    IHttpContextAccessor httpHttpContextAccessor, IProjectQueryService projectQueryService,
    IUserQueryService userQueryService)
    : IRequestHandler<CreateProjectApplicationCommand,Unit>
{
    public async Task<Unit> Handle(CreateProjectApplicationCommand request,  CancellationToken cancellationToken)
    {
        if (request.ProjectId <= 0) throw new ArgumentException("Invalid Project ID", nameof(request.ProjectId));

        var userId = httpHttpContextAccessor.HttpContext.User.Claims
                         .FirstOrDefault(c => c.Type == "Id")?.Value ??
                     throw new ArgumentException("user null");

        var project = await projectQueryService.Set(pr => pr.Id == request.ProjectId).FirstOrDefaultAsync(cancellationToken)
                      ?? throw new ArgumentException("Project does not exist");
        
        if(userId == project.PrincipalId)
            throw new  ArgumentException("Cannot apply to own project");

        var existingApplication = await applicationRepository.Set(pa => 
            pa.ApplicantId == userId && pa.ProjectId == request.ProjectId).FirstOrDefaultAsync(cancellationToken);
        
        if (existingApplication != null)
        {
            throw new InvalidOperationException("User has already applied for this project");
        }

        var applicant = await userQueryService.GetByAsync(u => u.Id == userId)
                        ?? throw new ArgumentException("Could not retrieve applicant");

        var principal = await userQueryService.GetByAsync
                            (p =>p.Id == project.PrincipalId)
                        ?? throw new ArgumentException("Could not retrieve principal");

        if (string.IsNullOrEmpty(applicant.UserName) || string.IsNullOrEmpty(principal.UserName))
            throw new ArgumentException("Username is empty");

        var application = mapper.Map<ProjectApplication>(request);
        
        application.ApplicantId = userId;
        application.PrincipalId = project.PrincipalId;
        application.Applicant = applicant.UserName;

        applicationRepository.Insert(application);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}