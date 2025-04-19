using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Requirements;

// public class GetRequirementTests(IRequirementRepository requirementRepository, IMapper mapper)
//     : IRequestHandler<GetRequirementTest, List<GetRequirementTestModel>>
// {
//     public async Task<List<GetRequirementTestModel>> Handle(GetRequirementTest request, CancellationToken cancellationToken)
//     {
//         // Fetch data for all the requirement names in the request
//         var requirementTests = await requirementRepository.Set()
//             .SelectMany(r => r.RequirementTests)
//             .Include(r => r.Requirement)
//             .Where(r => request.Name.Contains(r.Requirement.Name))  // Filter based on multiple names
//             .ToListAsync(cancellationToken);
//
//         // Map the result to the model
//         var result = mapper.Map<List<GetRequirementTestModel>>(requirementTests);
//
//         return result;
//     }
// }