using MediatR;


namespace Project_Backend_2024.Services.CommandServices.Projects.Delete;

public record DeleteProjectCommand(int Id) : IRequest<Unit>;