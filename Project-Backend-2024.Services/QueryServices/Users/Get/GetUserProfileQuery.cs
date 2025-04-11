using MediatR;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Services.QueryServices.Users.Get;

public record GetUserProfileQuery() : IRequest<GetUserProfileModel>{}