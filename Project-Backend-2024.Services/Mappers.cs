using Project_Backend_2024.Facade.Models;
using AutoMapper;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<User, UserModel>();
        CreateMap<UserModel, User>();/*.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));*/ //UserModel only maps properties to User that are not null

        CreateMap<Project, ProjectModel>();
        CreateMap<ProjectModel, Project>();/*.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
*/
        CreateMap<AppliedProject, AppliedProjectModel>();
        CreateMap<AppliedProjectModel, AppliedProject>();/*.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));*/

        CreateMap<UserSkills, UserSkillsModel>();
        CreateMap<UserSkillsModel, UserSkills>();

    }
}
