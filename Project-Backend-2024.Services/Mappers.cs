using Project_Backend_2024.Facade.Models;
using AutoMapper;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Services;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<User, UserModel>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
       //UserModel only maps properties to User that are not null

        CreateMap<RegisterUserModel, UserModel>();
        CreateMap<RegisterUserModel, User>();
        CreateMap<User, UserQueryModel>();

        CreateMap<Project, ProjectModel>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<AppliedProjectModel, AppliedProject>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UserSkills, UserSkillsModel>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
