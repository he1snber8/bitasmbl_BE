using MediatR;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.Users.List;

public record ListAllUsersQuery() : IRequest<List<GetUserModel>?>;