namespace Project_Backend_2024.DTO.Interfaces;

public interface IBaseEntity { }
public interface IEntity<out T> : IBaseEntity { T Id { get; } }
public interface IEntity :  IEntity<int> { }
