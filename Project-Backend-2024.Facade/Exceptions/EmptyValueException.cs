namespace Project_Backend_2024.Facade.Exceptions;

public class EmptyValueException : Exception
{
    public EmptyValueException()
        : base("Empty values are not allowed")
    {
    }

    public EmptyValueException(string fieldName)
        : base($"Empty value is not allowed for '{fieldName}'")
    {
    }

    public EmptyValueException(string fieldName, string message)
        : base($"Empty value is not allowed for '{fieldName}': {message}")
    {
    }

    public EmptyValueException(string fieldName, string message, Exception innerException)
        : base($"Empty value is not allowed for '{fieldName}': {message}", innerException)
    {
    }
}
