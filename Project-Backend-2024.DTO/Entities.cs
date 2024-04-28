namespace Project_Backend_2024.DTO;

public interface IBasic { }

public interface IEntity : IBasic { int Id { get; } }

public interface IDeletable : IBasic { bool IsDeleted { get; set; } }

public interface IAuthenticatable /* interface for authenticatable entities like User */ : IBasic, IEntity {
     string? Username { get; set; } 
     string? Password { get; set; }
     string? Email { get; set; }
} 
