using MediatR;

namespace Project_Backend_2024.Services.CommandServices.ProjectApplications.Create;

public record CreateProjectApplicationCommand(string? CoverLetter, int ProjectId) : IRequest<Unit> { }
