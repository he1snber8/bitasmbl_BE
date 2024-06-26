using MediatR;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services.QueryServices.Skills.Get;

public record GetSkillByIdQuery(int Id) : IRequest<Skill>;