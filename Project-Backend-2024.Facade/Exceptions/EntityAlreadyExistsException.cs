namespace Project_Backend_2024.Facade.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(string entityName)
        : base($"{entityName} already exists")
    {
    }

   
    public EntityAlreadyExistsException(string? preverb,string entityName, string message)
        : base($"{preverb} [{entityName}] already exists{message}")
    {
    }

    public EntityAlreadyExistsException(string entityName, string message, Exception innerException)
        : base($"The {entityName} already exists: {message}", innerException)
    {
    }
}
