using MediatR;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Services.QueryServices.Skills.List;

public record ListSkillsQuery() : IRequest<List<GetSkillsModel>>;