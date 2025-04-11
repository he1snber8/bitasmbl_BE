using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Users.Get;

public class GetUserByIdQueryHandler(UserManager<User> userManager, IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, GetUserProfileModel?>
{
    public async Task<GetUserProfileModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Set()
            .Include(u => u.Projects)
            .Include(u => u.UserSocials)
            .Where(u => u.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var userModel = mapper.Map<GetUserProfileModel>(user);

        return userModel;
    }
}