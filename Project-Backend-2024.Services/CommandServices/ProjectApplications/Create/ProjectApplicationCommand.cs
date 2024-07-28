using MediatR;

namespace Project_Backend_2024.Services.CommandServices.ProjectApplications.Create;

public record ProjectApplicationCommand(string? CoverLetter, int ProjectId) : IRequest<Unit> { }
