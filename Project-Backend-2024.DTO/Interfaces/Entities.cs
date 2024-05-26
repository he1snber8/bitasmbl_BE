namespace Project_Backend_2024.DTO.Interfaces;

public interface IEntity<out T> { T Id { get; } }

public interface IEntity :  IEntity<int> { }

public interface IDeletable { bool IsDeleted { get; set; } }

public interface IAuthenticatable /* interface for authenticatable entities like User */
{
    string Username { get; set; }
    string Password { get; set; }
}

public interface IMailApplicable { string Email { get; set; } }
