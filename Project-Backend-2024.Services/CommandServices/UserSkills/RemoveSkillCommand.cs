// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Facade.Interfaces;
//
// namespace Project_Backend_2024.Services.CommandServices.UserSkills;
//
// public class RemoveSkillCommand(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<ApplySkillModel, Unit>
// {
//     public async Task<Unit> Handle(ApplySkillModel request, CancellationToken cancellationToken)
//     {
//         var skill = await unitOfWork.SkillRepository.Set(s => s.Name == request.Name).FirstOrDefaultAsync(cancellationToken)
//                     ?? throw new ArgumentException("Could not find skill");
//         
//         var httpUser = httpContextAccessor.HttpContext.User;
//
//         if (httpUser.Identity is null)
//             throw new ArgumentNullException(nameof(httpUser), "Could not retrieve user");
//         
//         var userId = httpUser.Claims.FirstOrDefault(c => c.Type == "Id")?.Value 
//                      ?? throw new UserNotFoundException();
//
//         var userSkill = unitOfWork.UserSkillsRepository.Set(us => us.UserId == userId && us.SkillId == skill.Id)
//                         ?? throw new UserSkillException();
//
//         unitOfWork.UserSkillsRepository.Delete(userSkill);
//
//         return Unit.Value;
//     }
// }