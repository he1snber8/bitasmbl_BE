
using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Exceptions;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Services.Models;

namespace Project_Backend_2024.Services.CommandServices;

public abstract class BaseCommandService<TEntityModel, TEntity, TRepository> : ICommandModel<TEntityModel>
    where TEntityModel : class, IEntityModel
    where TEntity : class, IEntity
    where TRepository : IRepositoryBase<TEntity>
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly TRepository _repository;

    public BaseCommandService(IUnitOfWork unitOfWork, IMapper mapper, TRepository repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
    }

    public virtual async Task<int> Delete(int id)
    {
        var entity  = _repository.Set(u => u.Id == id).SingleOrDefault()
           ?? throw new KeyNotFoundException();

        if (entity is IDeletable deletableEntity) { deletableEntity.IsDeleted = true; }

        await _unitOfWork!.SaveChangesAsync();

        return entity.Id;

    }

    public virtual async Task<int> Insert(TEntityModel model)
    {
        if (model is null) throw new ArgumentNullException("Inserted entity must not be null!");

        TEntity entity = _mapper.Map<TEntity>(model);

        _repository.Insert(entity);
        await _unitOfWork.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task Update(int id, TEntityModel model)
    {
        var entity = _repository.Set(u => u.Id == id).FirstOrDefault() ??
            throw new KeyNotFoundException();;
  
        _mapper.Map(model, entity);

        if (entity is IDeletable deletableEntity && deletableEntity.IsDeleted)
            throw new EntityNotFoundException<TEntity>(id);

        await _unitOfWork.SaveChangesAsync();
    }
}
