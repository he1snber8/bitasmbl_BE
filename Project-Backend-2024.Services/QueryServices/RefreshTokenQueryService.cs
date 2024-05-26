using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Services.QueryServices;

public class RefreshTokenQueryService : BaseQueryService<RefreshToken, RefreshTokenModel, IRefreshTokenRepository> , IRefreshTokenQueryService
{
    public RefreshTokenQueryService(IUnitOfWork unitOfWork, IMapper mapper, IRefreshTokenRepository repository) 
        : base(unitOfWork, mapper, repository) { }

    public async Task<RefreshToken?> GetByTokenAsync(string token) => 
        await Task.FromResult(_repository.Set(u => u.Token == token).FirstOrDefault());

    public async Task<RefreshToken?> GetByUserAsync(string userId) =>
        await Task.FromResult(_repository.Set(u => u.UserID == userId).FirstOrDefault());

}