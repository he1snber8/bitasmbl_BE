using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.CommandServices;

public class RefreshTokenCommandService : BaseCommandService<RefreshTokenModel, RefreshToken, IRefreshTokenRepository>, IRefreshTokenCommandService
{
    public RefreshTokenCommandService(IUnitOfWork unitOfWork, IMapper mapper, IRefreshTokenRepository repository) : base(unitOfWork, mapper, repository) { }

    public async Task<RefreshToken> GetByToken(string token)
    {
        return await Task.FromResult(_repository.Set(u => u.Token == token).FirstOrDefault()) ??
            throw new KeyNotFoundException();
    }

    public async Task<RefreshToken> GetById(int id)
    {
        return await Task.FromResult(_repository.Set(u => u.Id == id).FirstOrDefault())??
            throw new KeyNotFoundException();
    }

    public async Task<RefreshToken?> GetByUserId(int id)
    {
        return await Task.FromResult(_repository.Set(u => u.UserID == id).FirstOrDefault());
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

        (bool isNull, bool IsUnactiveOrExpired, RefreshToken token) = await CheckTokenHealth(userId);

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

        else if (IsUnactiveOrExpired)
        {
            token.isActive = isActive;
            token.Token = updateToken;
            token.ExpirationDate = ExpirationDate;
            isAltered = true;
        }

        await _unitOfWork.SaveChangesAsync();

        return (token,isAltered);
    }

    private async Task<(bool,bool,RefreshToken)> CheckTokenHealth(int userId)
    {
        var token = await GetByUserId(userId);

        return (token is null, 
            token!.ExpirationDate < DateTime.Now &&
            token.isActive is false, token);
    }


}
