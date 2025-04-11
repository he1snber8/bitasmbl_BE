using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Skills.List;

public class ListSkillsQueryHandler(ISkillRepository skillRepository,IMapper mapper) : IRequestHandler<ListSkillsQuery, List<GetSkillsModel>>
{
    public async Task<List<GetSkillsModel>> Handle(ListSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await skillRepository.Set().ToListAsync(cancellationToken);

        var skillModels = mapper.Map<List<GetSkillsModel>>(skills);

        return skillModels;
    }
}