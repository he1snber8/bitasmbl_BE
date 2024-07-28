using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Projects.Create;

public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
    IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache, ILogger<CreateProjectCommandHandler> logger)
    : IRequestHandler<CreateProjectCommand, Unit>
{
    
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
            throw new EmptyValueException("Name");
        
        if (string.IsNullOrEmpty(request.Description))
            throw new EmptyValueException("Description");

        var httpUser = httpContextAccessor.HttpContext.User;

        if (httpUser.Identity is null)
            throw new ArgumentNullException(nameof(httpUser), "Could not retrieve user");

        var existingProject = await unitOfWork.ProjectRepository.Set(p => p.Name == request.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProject is not null)
            throw new EntityAlreadyExistsException(existingProject.Name);

        var model = mapper.Map<Project>(request);

        var userId = httpUser.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
                     ?? throw new UserNotFoundException();

        model.PrincipalId = userId;

        unitOfWork.ProjectRepository.Insert(model);

        await unitOfWork.SaveChangesAsync();

        memoryCache.Remove("SupaCache");
        Console.WriteLine("cache removed!");

        logger.LogInformation("[{Date}] User with an ID {userId} created project successfully.",
            DateTime.Now, userId);

        return Unit.Value;
    }
    
    
}