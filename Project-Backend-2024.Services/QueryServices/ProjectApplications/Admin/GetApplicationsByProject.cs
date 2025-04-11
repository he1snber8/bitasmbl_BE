using MediatR;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.ProjectApplications.Admin;


public record GetApplicationsByProjectQuery() : IRequest<List<ProjectApplicationModel>>;