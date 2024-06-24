using AutoMapper;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Exceptions;

namespace Project_Backend_2024.Services.CommandServices;

public abstract class BaseCommandService<TEntityModel, TEntity, TRepository> : ICommandModel<TEntityModel>
    where TEntityModel : class, IEntityModel
    where TEntity : class, IEntity
    where TRepository : IRepositoryBase<TEntity>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    protected readonly TRepository ApplicationRepository;
    public BaseCommandService(IUnitOfWork unitOfWork, IMapper mapper, TRepository applicationRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        ApplicationRepository = applicationRepository;
    }

    public virtual async Task<int> Delete(int id)
    {
        TEntity entity  = ApplicationRepository.Set(u => u.Id == id).SingleOrDefault()
           ?? throw new KeyNotFoundException();

        if (entity is IDeletable deletableEntity) { deletableEntity.IsDeleted = true; }

        await _unitOfWork!.SaveChangesAsync();

        return entity.Id;

    }

    public virtual async Task Insert(TEntityModel model)
    {
        if (model is null) throw new ArgumentNullException("Inserted entity must not be null!");

        TEntity entity = _mapper.Map<TEntity>(model);

        ApplicationRepository.Insert(entity);

        await _unitOfWork.SaveChangesAsync();
    }

    public virtual async Task Update(int id, TEntityModel model)
    {
        var entity = ApplicationRepository.Set(u => u.Id == id).FirstOrDefault() ??
            throw new KeyNotFoundException();;
  
        _mapper.Map(model, entity);

        if (entity is IDeletable deletableEntity && deletableEntity.IsDeleted)
            throw new Exception();

        await _unitOfWork.SaveChangesAsync();
    }

}
