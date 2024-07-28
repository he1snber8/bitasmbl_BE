using MediatR;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.Users.Get;

public record GetUserByEmailQuery(string Email) : IRequest<GetUserModel?>;