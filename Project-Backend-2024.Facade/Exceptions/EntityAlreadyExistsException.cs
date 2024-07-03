namespace Project_Backend_2024.Facade.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException()
        : base("Entity already exists")
    {
    }

    public EntityAlreadyExistsException(string entityName)
        : base($"{entityName} already exists, try again...")
    {
    }

    public EntityAlreadyExistsException(string entityName, string message)
        : base($"The {entityName} already exists: {message}")
    {
    }

    public EntityAlreadyExistsException(string entityName, string message, Exception innerException)
        : base($"The {entityName} already exists: {message}", innerException)
    {
    }
}
