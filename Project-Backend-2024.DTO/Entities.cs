namespace Project_Backend_2024.DTO;

public interface IEntity { int Id { get; } }

public interface IDeletable { bool IsDeleted { get; set; } }

public interface IAuthenticatable /* interface for authenticatable entities like User */ : IEntity {
     string? Username { get; set; } 
     string? Password { get; set; }
     string? Email { get; set; }
} 
