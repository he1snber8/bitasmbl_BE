// using AutoMapper;
// using MediatR;
// using Microsoft.Extensions.Caching.Memory;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade.Interfaces;
// using Project_Backend_2024.Facade.GetModels;
// using Project_Backend_2024.Services.Caching;
//
// namespace Project_Backend_2024.Services.QueryServices.Users.List;
//
// public class ListAllUsersQueryHandler(IUserRepository userRepository,IMemoryCache cache,CachingService cachingService,IMapper mapper) : 
//     QueryCachingService<List<User>?,List<GetUserModel>?>(cache,cachingService,mapper),
//     IRequestHandler<ListAllUsersQuery,List<GetUserModel>?>
// {
//     public async Task<List<GetUserModel>?> Handle(ListAllUsersQuery request, CancellationToken cancellationToken)
//     {
//         var users = await userRepository.GetAll();
//
//         var userModels = InitializeQueryCaching(users.Count.ToString(), users);
//
//         return userModels;
//     }
// }