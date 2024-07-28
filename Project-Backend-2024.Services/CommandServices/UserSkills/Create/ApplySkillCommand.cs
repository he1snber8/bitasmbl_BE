using MediatR;

namespace Project_Backend_2024.Services.CommandServices.UserSkills.Create;

public record ApplySkillCommand(string Name) : IRequest<Unit>;