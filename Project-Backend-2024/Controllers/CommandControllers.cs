using Azure.Core;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Controllers;

public class UserSkillsController : BaseCommandController<UserSkillsModel, IUserSkillsCommandService>
{
    public UserSkillsController(IUserSkillsCommandService commandService) : base(commandService) { }

}

public class ProjectController : BaseCommandController<ProjectModel, IProjectCommandService>
{
    public ProjectController(IProjectCommandService commandService) : base(commandService) { }
}

public class AppliedProjectController : BaseCommandController<AppliedProjectModel, IAppliedProjectCommandService>
{
    public AppliedProjectController(IAppliedProjectCommandService commandService) : base(commandService) { }
}

public class SkillController : BaseCommandController<SkillModel, ISkillCommandService>
{
    public SkillController(ISkillCommandService commandService) : base(commandService) { }
}


