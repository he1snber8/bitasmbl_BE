// using AutoMapper;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.Interfaces;
//
// namespace Project_Backend_2024.Services.CommandServices.Skills;
//
// public class DeleteSkillCommand(IUnitOfWork unitOfWork)
//     : IRequestHandler<RemoveSkillModel, Unit>
// {
//     public async Task<Unit> Handle(RemoveSkillModel request, CancellationToken cancellationToken)
//     {
//         var skill = await unitOfWork.SkillRepository.Set(s => s.Id == request.Id)
//                         .FirstOrDefaultAsync(cancellationToken)
//                     ?? throw new ArgumentException($"{request.Id} was not found");
//
//         unitOfWork.SkillRepository.Delete(skill);
//          
//         await unitOfWork.SaveChangesAsync();
//           
//         return Unit.Value;
//     }
// }