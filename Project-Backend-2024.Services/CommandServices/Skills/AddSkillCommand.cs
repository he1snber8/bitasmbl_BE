// using AutoMapper;
// using MediatR;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.Interfaces;
//
// namespace Project_Backend_2024.Services.CommandServices.Skills;
//
// public class AddSkillCommand(IUnitOfWork unitOfWork)
//     : IRequestHandler<AddSkillModel, Unit>
// {
//     public async Task<Unit> Handle(AddSkillModel request, CancellationToken cancellationToken)
//     {
//         unitOfWork.SkillRepository.Insert(new Skill() { Name = request.Name });
//
//         await unitOfWork.SaveChangesAsync();
//
//         return Unit.Value;
//     }
// }