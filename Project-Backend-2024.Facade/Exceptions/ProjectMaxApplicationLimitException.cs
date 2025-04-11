namespace Project_Backend_2024.Facade.Exceptions;

public class ProjectMaxApplicationLimitException(string projectName, byte currentApplications)
    : Exception(GenerateMessage(projectName, currentApplications))
{
    private static string GenerateMessage(string projectName, byte currentApplications)
        => $"{projectName} has reached max amount of allowed applications: {currentApplications}";
}