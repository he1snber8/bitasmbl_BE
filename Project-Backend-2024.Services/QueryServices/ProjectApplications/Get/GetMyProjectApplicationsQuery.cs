using MediatR;
using Project_Backend_2024.Facade.BasicGetModels;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

public record GetMyProjectApplicationsQuery() : IRequest<List<ProjectApplicationBasicGetModel>>;