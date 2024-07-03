using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices.Users.Admin.Get;

public class GetUserByEmailQueryHandler(IMemoryCache cache,CachingService cachingService,IMapper mapper,UserManager<User> userManager) : 
    QueryCachingService<User,UserModel>(cache,cachingService,mapper),
        IRequestHandler<GetUserByEmailQuery, UserModel?>
{
    public async Task<UserModel?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new UserNotFoundException("Could not find user with given email");

        var userModel = InitializeQueryCaching($"key-{user.Id}",user);
        
        return userModel;
    }
}