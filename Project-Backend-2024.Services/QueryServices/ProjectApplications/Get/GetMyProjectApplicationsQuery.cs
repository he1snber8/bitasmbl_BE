using MediatR;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

public record GetMyProjectApplicationsQuery(int? Id) : IRequest<List<GetProjectApplicationModel?>>;