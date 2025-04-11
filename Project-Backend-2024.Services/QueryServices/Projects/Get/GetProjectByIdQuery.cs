using MediatR;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.Get;

public record GetProjectByIdQuery(int Id) : IRequest<GetUserProjectModel?>;