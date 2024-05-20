
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;
using System.Linq.Expressions;

namespace Project_Backend_2024.Services.QueryServices;

public abstract class BaseQueryService<TEntity, TEntityModel,TRepository> : IQueryModel<TEntity, TEntityModel>
    where TEntity : class, IEntity
    where TRepository : IRepositoryBase<TEntity>
    where TEntityModel : class, IEntityModel
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly TRepository _repository;

    public BaseQueryService(IUnitOfWork unitOfWork, IMapper mapper, TRepository repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<TEntityModel> GetByIdAsync(int id) => await _mapper.Map<Task<TEntityModel>>(_repository.Set(u => u.Id == id).SingleOrDefault());

    public virtual IQueryable<TEntityModel> Set(Expression<Func<TEntity, bool>> predicate)
    {
        return _repository.Set(predicate).ProjectTo<TEntityModel>(_mapper.ConfigurationProvider);
    }

    public async Task<List<TEntityModel>> GetAll()
    {
        var entities = await _repository.GetAll();

        var models = _mapper.Map<List<TEntityModel>>(entities);

        return models;
    }

    public IQueryable<TEntityModel> Set() => _repository.Set().ProjectTo<TEntityModel>(_mapper.ConfigurationProvider);
}
