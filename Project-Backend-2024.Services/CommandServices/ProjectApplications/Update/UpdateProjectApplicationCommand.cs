using MediatR;

namespace Project_Backend_2024.Services.CommandServices.ProjectApplications.Update;

public record UpdateProjectApplicationCommand(int Id,string? ApplicationStatus) : IRequest<Unit>;