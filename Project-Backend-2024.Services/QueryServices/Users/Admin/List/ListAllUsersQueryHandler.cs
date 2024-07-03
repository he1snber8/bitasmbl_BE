using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Users.Admin.List;

public class ListAllUsersQueryHandler(IUserRepository userRepository,IMemoryCache cache,CachingService cachingService,IMapper mapper) : 
    QueryCachingService<List<User>?,List<UserModel>?>(cache,cachingService,mapper),
    IRequestHandler<ListAllUsersQuery,List<UserModel>?>
{
    public async Task<List<UserModel>?> Handle(ListAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAll();

        var userModels = InitializeQueryCaching($"key-users", users);

        return userModels;
    }
}