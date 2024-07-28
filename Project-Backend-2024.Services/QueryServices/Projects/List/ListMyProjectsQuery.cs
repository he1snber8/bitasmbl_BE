using MediatR;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.List;

public record ListMyProjectsQuery() : IRequest<List<GetProjectModel?>?>;