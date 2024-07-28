using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.InsertModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Skills.List;

public class ListUserSkillsQueryHandler(IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<ListUserSkillsQuery, List<SkillModel>>
{
    public async Task<List<SkillModel>> Handle(ListUserSkillsQuery request, CancellationToken cancellationToken)
    {
        var userId =  httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;

        // Retrieve all the skill IDs associated with the user from the UserSkills table
        var userSkills = await unitOfWork.UserSkillsRepository
            .Set(us => us.UserId == userId)
            .Select(us => us.SkillId)
            .ToListAsync(cancellationToken);

        // Retrieve the corresponding skills based on the skill IDs
        var skills = await unitOfWork.SkillRepository
            .Set(s => userSkills.Contains(s.Id))
            .ToListAsync(cancellationToken);

        // Map the retrieved skills to SkillModel
        var skillModels = mapper.Map<List<SkillModel>>(skills);

        return skillModels;
    }
}