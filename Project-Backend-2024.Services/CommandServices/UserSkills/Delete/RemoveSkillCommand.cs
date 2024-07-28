using MediatR;

namespace Project_Backend_2024.Services.CommandServices.UserSkills.Delete;

public record RemoveSkillCommand(string Name) : IRequest<Unit>;