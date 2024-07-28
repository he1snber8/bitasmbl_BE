using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Skills.Delete;

public class DeleteSkillCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSkillCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await unitOfWork.SkillRepository.Set(s => s.Name == request.Name)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new ArgumentException($"{request.Name} was not found");

        unitOfWork.SkillRepository.Delete(skill);
         
        await unitOfWork.SaveChangesAsync();
          
        return Unit.Value;
    }
}