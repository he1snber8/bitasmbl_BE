using MediatR;

namespace Project_Backend_2024.Services.CommandServices.UserSkills.Create;

public record CreateUserSkillCommand(string Name) : IRequest<Unit>;