
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Authentication.Authorization;

public class AuthorizableService : IAuthorizableService
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthorizableService(IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public Task<(bool, UserModel)> AutheticateLogin(IAuthenticatable loginModel)
    {
        throw new Exception();
    }

    public Task<bool> ValidateCredentials(UserModel model)
    {
        throw new NotImplementedException();
    }

    public async Task Register(RegisterUserModel registerModel)
    {
        var model = _mapper.Map<User>(registerModel);

        await _userManager.CreateAsync(model);

    }
}
