using Project_Backend_2024.DTO.Enums;

namespace Project_Backend_2024.Facade.Exceptions;

public class InvalidProjectStatusException(ProjectStatus status) : Exception(GenerateMessage(status))
{
    private static string GenerateMessage(ProjectStatus status)
    {
        return status switch
        {
            ProjectStatus.Active => "The project is currently active and cannot be modified in this way.",
            ProjectStatus.Inactive => "The project is inactive. Cannot apply unless activated by the creator",
            ProjectStatus.Completed => "The project has been completed and cannot be altered.",
            ProjectStatus.Cancelled => "The project has been cancelled and cannot be modified.",
            ProjectStatus.OnHold => "The project is on hold. Please resume it before proceeding.",
            ProjectStatus.Archived => "The project has been archived and cannot be modified.",
            ProjectStatus.Draft => "The project is in draft status and not yet finalized.",
            _ => "The project has an unknown status.",
        };
    }
}