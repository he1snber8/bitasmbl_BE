
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Models;

namespace Project_Backend_2024.Services.Interfaces.Queries;

public interface IProjectQueryService : IQueryModel<Project, ProjectModel> { }

public interface IAppliedProjectQueryService : IQueryModel<AppliedProject, AppliedProjectModel> { }

public interface IUserSkillsQueryService : IQueryModel<UserSkills, UserSkillsModel> { }

public interface ISkillQueryService : IQueryModel<Skill, SkillModel> { }

public interface IUserQueryService
{
    IQueryable<UserModel> GetAll();
    Task<UserModel> GetByEmailAsync(string email);
    Task<UserModel> GetByUsernameAsync(string username);
}