using MediatR;
namespace Project_Backend_2024.Services.CommandServices.Projects.Update;

public record UpdateProjectCommand(int Id, string? Name, string? Description, string? Status) : IRequest<Unit>;