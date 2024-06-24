using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Project_Backend_2024.Services.QueryServices;

public abstract class BaseQueryService<TEntity, TEntityModel,TRepository> : IQueryModel<TEntity, TEntityModel>
    where TEntity : class, IEntity
    where TRepository : IRepositoryBase<TEntity>
    where TEntityModel : class, IEntityModel
{
    protected readonly IMapper _mapper;
    protected readonly TRepository ApplicationRepository;

    public BaseQueryService(IMapper mapper, TRepository applicationRepository)
    {
        _mapper = mapper;
        ApplicationRepository = applicationRepository;
    }

    public virtual async Task<TEntityModel> GetByIdAsync(int id)
    {
        var entity = await ApplicationRepository.Set(u => u.Id == id).SingleOrDefaultAsync();
        return _mapper.Map<TEntityModel>(entity);
    }

    public virtual IQueryable<TEntityModel> Set(Expression<Func<TEntity, bool>> predicate)
    {
        return ApplicationRepository.Set(predicate).ProjectTo<TEntityModel>(_mapper.ConfigurationProvider);
    }

    public async Task<List<TEntityModel>> GetAll()
    {
        var entities = await ApplicationRepository.GetAll();

        var models = _mapper.Map<List<TEntityModel>>(entities);

        return models;
    }

    public IQueryable<TEntityModel> Set() => ApplicationRepository.Set().ProjectTo<TEntityModel>(_mapper.ConfigurationProvider);
}
