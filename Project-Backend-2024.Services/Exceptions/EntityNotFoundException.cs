
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services.Exceptions;

[Serializable]
public class EntityNotFoundException<TEntity> : Exception
    where TEntity : IEntity
{
    public EntityNotFoundException(int id) : base($"Entity with an ID of {id} is either deleted or inactive") { }
}
