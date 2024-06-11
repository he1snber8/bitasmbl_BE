using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Services.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserQueryService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserQueryModel> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return _mapper.Map<UserQueryModel>(user);
    }

    public async Task<UserQueryModel> GetByUsernameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return _mapper.Map<UserQueryModel>(user);
    }

    public IQueryable<UserQueryModel> GetAll()
    {
        var users = _userManager.Users;
        return _mapper.ProjectTo<UserQueryModel>(users);
    }

    public IQueryable<UserModel> AdminGetAll()
    {
        var users = _userManager.Users;
        return _mapper.ProjectTo<UserModel>(users);
    }
}