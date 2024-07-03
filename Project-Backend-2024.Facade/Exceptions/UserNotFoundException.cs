namespace Project_Backend_2024.Facade.Exceptions;

public class UserNotFoundException : Exception
{
    private string? UserId { get; }
    public UserNotFoundException() : base("User does not exist") {}
    
    public UserNotFoundException(string? userId) 
        : base($"User with ID '{userId}' does not exist")
    {
        UserId = userId;
    }

    public UserNotFoundException(string? userId, string message) 
        : base(message)
    {
        UserId = userId;
    }

    public UserNotFoundException(string? userId, string message, Exception innerException) 
        : base(message, innerException)
    {
        UserId = userId;
    }
    
    public override string Message => UserId is null ? base.ToString() : $"User with ID '{UserId}' does not exist.";
}