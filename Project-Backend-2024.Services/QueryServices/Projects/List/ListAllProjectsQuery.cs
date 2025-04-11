using MediatR;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.List;

public record ListAllProjectsQuery() : IRequest<List<GetClientProjectModel?>?>;