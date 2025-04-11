namespace Project_Backend_2024.Facade.Exceptions;

public class EmailValidationException() : Exception
{
    public override string ToString()
    {
        return $"Email is in incorrect format";
    }
}