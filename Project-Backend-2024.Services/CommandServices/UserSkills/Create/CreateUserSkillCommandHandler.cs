using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.UserSkills.Create;

public class CreateUserSkillCommandHandler(IUserSkillsRepository userSkillsRepository, IUnitOfWork unitOfWork, 
    ISkillRepository skillRepository, IHttpContextAccessor httpContextAccessor) 
    : IRequestHandler<CreateUserSkillCommand,Unit>
{
    public async Task<Unit> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await skillRepository.Set(s => s.Name == request.Name).FirstOrDefaultAsync(cancellationToken)
                    ?? throw new ArgumentException("Could not find skill");
        
        var httpUser = httpContextAccessor.HttpContext.User;

        if (httpUser.Identity is null)
            throw new ArgumentNullException(nameof(httpUser), "Could not retrieve user");
        
        var userId = httpUser.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
                     ?? throw new UserNotFoundException();

        userSkillsRepository.Insert(new DTO.UserSkills()
        {
            SkillId = skill.Id,
            UserId = userId
        });

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}