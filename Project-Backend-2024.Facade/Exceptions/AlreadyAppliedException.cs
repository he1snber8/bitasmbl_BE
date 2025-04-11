namespace Project_Backend_2024.Facade.Exceptions;

public class AlreadyAppliedException(string projectName)
    : Exception(GenerateMessage(projectName))
{
    private static string GenerateMessage(string projectName)
        => $"you have already applied to {projectName}";
}