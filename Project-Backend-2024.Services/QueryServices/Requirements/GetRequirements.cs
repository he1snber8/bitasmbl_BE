using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Requirements;

public class GetRequirements(IRequirementRepository requirementRepository)
    : IRequestHandler<GetRequirementsModel, List<Requirement>>
{
    public async Task<List<Requirement>> Handle(GetRequirementsModel request, CancellationToken cancellationToken) =>
        await requirementRepository.GetAll();
}