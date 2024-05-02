namespace Project_Backend_2024.DTO.Interfaces;

public interface IEntity { int Id { get; } }

public interface IDeletable { bool IsDeleted { get; set; } }

public interface IAuthenticatable /* interface for authenticatable entities like User */
{
    string Username { get; set; }
    string Password { get; set; }
}

public interface IMailApplicable { string Email { get; set; } }
