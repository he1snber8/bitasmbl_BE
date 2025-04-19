// using AutoMapper;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.Interfaces;
//
// namespace Project_Backend_2024.Services.QueryServices.Skills.List;
//
// public class ListUserSkillsQueryHandler(IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<ListUserSkillsQuery, List<ApplySkillModel>>
// {
//     public async Task<List<ApplySkillModel>> Handle(ListUserSkillsQuery request, CancellationToken cancellationToken)
//     {
//         var userId =  httpContextAccessor.HttpContext.User.Claims
//             .FirstOrDefault(c => c.Type == "Id")?.Value;
//
//         // Retrieve all the skill IDs associated with the user from the UserSkills table
//         var userSkills = await unitOfWork.UserSkillsRepository
//             .Set(us => us.UserId == userId)
//             .Select(us => us.SkillId)
//             .ToListAsync(cancellationToken);
//
//         // Retrieve the corresponding skills based on the skill IDs
//         var skills = await unitOfWork.SkillRepository
//             .Set(s => userSkills.Contains(s.Id))
//             .ToListAsync(cancellationToken);
//
//         // Map the retrieved skills to SkillModel
//         var skillModels = mapper.Map<List<ApplySkillModel>>(skills);
//
//         return skillModels;
//     }
// }