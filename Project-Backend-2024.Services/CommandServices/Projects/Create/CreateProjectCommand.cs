using MediatR;


namespace Project_Backend_2024.Services.CommandServices.Projects.Create;

public record CreateProjectCommand(string? Name, string? Description) : IRequest<Unit>;