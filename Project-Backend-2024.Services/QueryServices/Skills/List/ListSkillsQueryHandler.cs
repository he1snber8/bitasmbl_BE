using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade.InsertModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Skills.List;

public class ListSkillsQueryHandler(ISkillRepository skillRepository,IMapper mapper) : IRequestHandler<ListSkillsQuery, List<SkillModel>>
{
    public async Task<List<SkillModel>> Handle(ListSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await skillRepository.Set().ToListAsync(cancellationToken);

        var skillModels = mapper.Map<List<SkillModel>>(skills);

        return skillModels;
    }
}