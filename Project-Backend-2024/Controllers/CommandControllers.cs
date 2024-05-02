using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.TokenGenerators.Interfaces;

namespace Project_Backend_2024.Controllers;

public class UserController : BaseCommandController<UserModel, IUserCommandService>
{
    private readonly IUserAccessToken _accessTokenGenerator;

    public UserController(IUserCommandService command, IUserAccessToken accessTokenGenerator) : base(command)
    { _accessTokenGenerator = accessTokenGenerator; }

    public override async Task<IActionResult> Insert([FromBody] UserModel model)
    {
        try
        {
            return await base.Insert(model);
        }
        catch (UsernameValidationException ex)
        {
            return BadRequest($"Username validation failed: {ex.Message}");
        }
        catch (PasswordValidationException ex)
        {
            return BadRequest($"Password validation failed: {ex.Message}");
        }
        catch (EmailValidationException ex)
        {
            return BadRequest($"Email validation failed: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public IActionResult LogIn([FromBody] UserLoginModel loginModel)
    {
        (bool isAuthenticated, User user) = _commandService.AutheticateLogin(loginModel);

        if (isAuthenticated)
        {
           string token = _accessTokenGenerator.GenerateToken(user);

           return Ok(token);

        }
        return Unauthorized();
    }

}

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


