namespace Project_Backend_2024.Facade.Exceptions;

public class ProjectNotFoundException(int? projectId = null, string? projectName = null)
    : Exception(GenerateMessage(projectId, projectName))
{
    private static string GenerateMessage(int? projectId, string? projectName)
    {
        if (projectId.HasValue)
        {
            return $"Project with ID {projectId} does not exist.";
        }
        if (!string.IsNullOrEmpty(projectName))
        {
            return $"Project with name '{projectName}' does not exist.";
        }
        return "Project does not exist.";
    }
}