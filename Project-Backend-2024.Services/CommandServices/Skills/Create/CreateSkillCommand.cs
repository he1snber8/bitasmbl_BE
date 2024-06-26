using MediatR;

namespace Project_Backend_2024.Services.CommandServices.Skills.Create;

public record CreateSkillCommand(string Name) : IRequest<Unit>;