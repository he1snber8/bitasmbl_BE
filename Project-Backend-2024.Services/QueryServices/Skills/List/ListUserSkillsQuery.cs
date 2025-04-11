using MediatR;
using Project_Backend_2024.Facade;


namespace Project_Backend_2024.Services.QueryServices.Skills.List;

public record ListUserSkillsQuery() : IRequest<List<ApplySkillModel>>;