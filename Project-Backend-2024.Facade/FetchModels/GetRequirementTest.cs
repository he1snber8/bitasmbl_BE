using MediatR;

namespace Project_Backend_2024.Facade.FetchModels;

public record GetRequirementTest(string Name) : IRequest<List<GetRequirementTestModel>>;
