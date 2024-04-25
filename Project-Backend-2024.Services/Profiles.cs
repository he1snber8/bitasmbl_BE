using Project_Backend_2024.Services.Models;
using AutoMapper;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<User, UserModel>();
        CreateMap<Project, ProjectModel>();
        CreateMap<AppliedProject, AppliedProjectModel>();
        CreateMap<UserSkills, UserSkillsModel>();
    }
}
