using MediatR;

namespace Project_Backend_2024.Services.CommandServices.Skills.Delete;

public record DeleteSkillCommand(string Name) : IRequest<Unit>;