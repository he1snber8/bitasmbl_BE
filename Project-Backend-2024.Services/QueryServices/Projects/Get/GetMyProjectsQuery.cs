using MediatR;
using Project_Backend_2024.Facade.BasicGetModels;
using Project_Backend_2024.Facade.BasicInsertModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public record GetMyProjectsQuery() : IRequest<List<ProjectBasicGetModel?>?>;