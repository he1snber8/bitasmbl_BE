using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.ProjectApplications.Update;

public class UpdateProjectApplicationCommandHandler(
    IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpHttpContextAccessor)
    : IRequestHandler<UpdateProjectApplicationCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProjectApplicationCommand request, CancellationToken cancellationToken)
    {
        var userId = httpHttpContextAccessor.HttpContext.User.Claims
                         .FirstOrDefault(c => c.Type == "Id")?.Value ??
                     throw new ArgumentException("user null");

        var projectApplication = await unitOfWork.ProjectApplicationRepository.Set(pr => pr.Id == request.Id).FirstOrDefaultAsync(cancellationToken)
                      ?? throw new ArgumentException("Application does not exist");

        if (projectApplication.PrincipalId != userId) throw new UnauthorizedAccessException("Request denied");

        mapper.Map(request, projectApplication);
        
        unitOfWork.ProjectApplicationRepository.Update(projectApplication);
        
        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}