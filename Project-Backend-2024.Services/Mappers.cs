using Project_Backend_2024.Services.Models;
using AutoMapper;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<User, UserModel>();
        CreateMap<UserModel, User>();

        CreateMap<Project, ProjectModel>();
        CreateMap<ProjectModel, Project>();

        CreateMap<AppliedProject, AppliedProjectModel>();
        CreateMap<AppliedProjectModel, AppliedProject>();

        CreateMap<UserSkills, UserSkillsModel>();
        CreateMap<UserSkillsModel, UserSkills>();

    }
}
