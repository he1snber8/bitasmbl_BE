// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Facade.Interfaces;
// using Project_Backend_2024.DTO;
//
// namespace Project_Backend_2024.Services.CommandServices.UserSkills;
//
// public class ApplySkillCommand(
//     IUnitOfWork unitOfWork,
//     IHttpContextAccessor httpContextAccessor,
//     IUserSkillsRepository userSkillsRepository)
//     : IRequestHandler<ApplySkillModel, Unit>
// {
//     public async Task<Unit> Handle(ApplySkillModel request, CancellationToken cancellationToken)
//     {
//         var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
//                      ?? throw new UserNotFoundException();
//         
//         var skill = await unitOfWork.SkillRepository.Set(s => s.Name == request.Name)
//                         .FirstOrDefaultAsync(cancellationToken)
//                     ?? throw new ArgumentException("Skill not found.");
//
//         userSkillsRepository.Insert(new DTO.UserSkills()
//         {
//             SkillId = skill.Id,
//             UserId = userId
//         });
//         
//         await unitOfWork.SaveChangesAsync();
//
//         return Unit.Value;
//     }
// }