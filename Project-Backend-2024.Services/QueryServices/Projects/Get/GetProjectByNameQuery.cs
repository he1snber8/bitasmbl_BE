using MediatR;
using Project_Backend_2024.Facade.BasicGetModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public record GetProjectByNameQuery(string? Name) : IRequest<ProjectBasicGetModel?>;
