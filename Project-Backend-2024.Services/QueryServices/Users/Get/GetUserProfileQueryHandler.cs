using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Users.Get;

public class GetUserProfileQueryHandler(
    UserManager<User> userManager,
    IMapper mapper,
    ITeamManagerRepository userRepository,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetUserProfileQuery, GetUserProfileModel>
{
    public async Task<GetUserProfileModel> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value ?? throw new UserNotFoundException();

        var user = await userRepository
            .Set(u => u.Id == userId)
            .Include(u => u.UserSocials)
            .SingleOrDefaultAsync(cancellationToken) ?? throw new UserNotFoundException();

        var userModel = mapper.Map<GetUserProfileModel>(user);

        // userModel.Skills =
        //     mapper.Map<List<GetSkillsModel>>
        //         (user.UserRequirementSkills.Where(us => us.User.Id == userId));
        
        return userModel;
    }
}