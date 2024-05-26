
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Interfaces.Commands;

public interface IUserCommandService : ICommandModel<UserModel>
{
    Task<(bool, UserModel)> AutheticateLogin(IAuthenticatable loginModel);
    Task<RegisterUserModel> Register(RegisterUserModel registerModel);
}

public interface IProjectCommandService : ICommandModel<ProjectModel> { }

public interface IAppliedProjectCommandService : ICommandModel<AppliedProjectModel> { }

public interface IUserSkillsCommandService : ICommandModel<UserSkillsModel> { }

public interface ISkillCommandService : ICommandModel<SkillModel> { }

public interface IRefreshTokenCommandService : ICommandModel<RefreshTokenModel>
{
    public Task InvalidateUserToken(string id);
    public Task<(RefreshToken,bool)> UpdateUserToken(string userId, DateTime ExpirationDate, string updateToken, bool isActive = true);
    //public Task<UserModel?> GetUserByToken(RefreshRequest refreshRequest);
}