namespace Project_Backend_2024.Facade;

public interface IBasic { }

public interface IEntity : IBasic { int Id { get; } } 

public interface IDeletable : IBasic { bool IsDeleted { get; set; } }
