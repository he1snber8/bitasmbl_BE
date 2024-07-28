using MediatR;
using Project_Backend_2024.Facade.AdminModels;

namespace Project_Backend_2024.Services.QueryServices.Users.Admin.Get;

public record GetUserByEmailQuery(string Email) : IRequest<UserModel> ;