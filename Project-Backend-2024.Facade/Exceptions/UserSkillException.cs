namespace Project_Backend_2024.Facade.Exceptions;

public class UserSkillException()
    : Exception(GenerateMessage())
{
    private static string GenerateMessage()
    {
        return "has no skills found, apply some!";
    }
}