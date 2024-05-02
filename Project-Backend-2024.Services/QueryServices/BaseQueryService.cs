
using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Queries;
using System.Linq.Expressions;

namespace Project_Backend_2024.Services.QueryServices;

public abstract class BaseQueryService<TEntity, TRepository> : IQueryModel<TEntity>
    where TEntity : class, IEntity
    where TRepository : IRepositoryBase<TEntity>
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

    public TEntity GetById(int id) =>
        _repository.Set(u => u.Id == id).SingleOrDefault() ?? throw new NotImplementedException();

    public IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate) => _repository.Set(predicate);

    public async Task<IEnumerable<TEntity>> GetAll() => await _repository.GetAll();

    public Task<IEnumerable<TEntity>> SetMany(Expression<Func<TEntity, bool>> predicate) => _repository.SetMany(predicate);
}
