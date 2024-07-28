using MediatR;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Get;

public record GetMyAppliedProjectsQuery() : IRequest<List<GetAppliedProjectsModel?>>;