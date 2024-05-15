using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Services.CommandServices;

public class RefreshTokenCommandService : BaseCommandService<RefreshTokenModel, RefreshToken, IRefreshTokenRepository>, IRefreshTokenCommandService
{
    private readonly IRefreshTokenQueryService _refreshTokenQueryService;
    private readonly IUserQueryService _userQueryService;
    
    public RefreshTokenCommandService(IUnitOfWork unitOfWork, IMapper mapper, IRefreshTokenRepository repository,
        IRefreshTokenQueryService refreshTokenQueryService, IUserQueryService userQueryService) : base(unitOfWork, mapper, repository)
    {
        _refreshTokenQueryService = refreshTokenQueryService;
        _userQueryService = userQueryService;
    }

    public async Task InvalidateUserToken(int id)
    {
        var token = await Task.FromResult(_repository.Set(u => u.UserID == id).FirstOrDefault())
            ?? throw new KeyNotFoundException();

        token.isActive = false;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(RefreshToken,bool)> UpdateUserToken(int userId, DateTime ExpirationDate,string updateToken, bool isActive = true)
    {
        bool isAltered = false;

        (bool isNull, bool IsInactiveOrExpired, RefreshToken? token) = await CheckTokenHealth(userId);

        if (isNull)
        {
            await Insert(new RefreshTokenModel
            {
                UserID = userId,
                ExpirationDate = ExpirationDate,
                Token = updateToken,
                isActive = isActive
            });

            isAltered = true;
        }

        else if (IsInactiveOrExpired is true)
        {
            token!.isActive = isActive;
            token.Token = updateToken;
            token.ExpirationDate = ExpirationDate;
            isAltered = true;
        }

        await _unitOfWork.SaveChangesAsync();

        return (token!,isAltered);
    }

    private async Task<(bool,bool,RefreshToken?)> CheckTokenHealth(int userId)
    {
        var token = await _refreshTokenQueryService.GetByUserAsync(userId);

        if (token is null) return (true, false, null);

        bool IsExpiredOrInactive = token!.ExpirationDate < DateTime.Now || token!.isActive is false;

        return (false, IsExpiredOrInactive, token);
    }

    public async Task<UserModel?> GetUserByToken(RefreshRequest refreshRequest)
    {
        var tokenByToken = await _refreshTokenQueryService.GetByTokenAsync(refreshRequest.refreshToken);

        if (tokenByToken is null || (tokenByToken.isActive is not true || tokenByToken.ExpirationDate < DateTime.Now)) return null;

        return await _userQueryService.GetByIdAsync(tokenByToken.UserID);
    }


}
