using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.UserSkills.Delete;

public class RemoveSkillCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RemoveSkillCommand, Unit>
{
    public async Task<Unit> Handle(RemoveSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await unitOfWork.SkillRepository.Set(s => s.Name == request.Name).FirstOrDefaultAsync(cancellationToken)
                    ?? throw new ArgumentException("Could not find skill");
        
        var httpUser = httpContextAccessor.HttpContext.User;

        if (httpUser.Identity is null)
            throw new ArgumentNullException(nameof(httpUser), "Could not retrieve user");
        
        var userId = httpUser.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
                     ?? throw new UserNotFoundException();

        var userSkill = unitOfWork.UserSkillsRepository.Set(us => us.UserId == userId && us.SkillId == skill.Id)
                        ?? throw new UserSkillException();

        unitOfWork.UserSkillsRepository.Delete(userSkill);

        return Unit.Value;
    }
}