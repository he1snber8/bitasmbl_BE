using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Skills.Get;

public class GetSkillByIdQueryHandler(ISkillRepository skillRepository) 
    : IRequestHandler<GetSkillByIdQuery,Skill> 
{
    public async Task<Skill> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        return await skillRepository.Set(s => s.Id == request.Id).FirstOrDefaultAsync(cancellationToken)
               ?? throw new ArgumentException("Could not find skill with given id");
    }
}