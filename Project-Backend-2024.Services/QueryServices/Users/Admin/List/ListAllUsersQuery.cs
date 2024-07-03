using MediatR;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.QueryServices.Users.Admin.List;

public record ListAllUsersQuery() : IRequest<List<UserModel>?>;