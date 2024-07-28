using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services.QueryServices.Users.Get;

public class GetUserByEmailQueryHandler(UserManager<User> userManager,IMapper mapper) : IRequestHandler<GetUserByEmailQuery, GetUserModel?>
{
    public async Task<GetUserModel?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        var userModel = mapper.Map<GetUserModel>(user);
        
        return userModel;
    }
}