using MediatR;
using Project_Backend_2024.Facade.AdminModels;

namespace Project_Backend_2024.Services.QueryServices.Projects.Admin.List;

public record ListAllProjectsQueryAdmin() : IRequest<List<ProjectModel>?>;