using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AdminModels;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Services;

public class Mappers : Profile
{
    public Mappers()
    {
        // User mappings
        CreateMap<User, UserModel>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<RegisterUserModel, UserModel>();
        CreateMap<RegisterUserModel, User>();
        CreateMap<GetUserProfileModel, User>().ReverseMap();
        CreateMap<User, GetUserModel>()
            .ReverseMap();
        // .ForMember(dest => dest, opt => opt.MapFrom(src => src.Email))
        // .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Project mappings
        CreateMap<Project, CreateProjectModel>().ReverseMap();
        CreateMap<Project, UpdateProjectModel>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Project, GetUserProjectModel>().ReverseMap()
            .ForMember(dest => dest.ProjectApplications, opt => opt.MapFrom(src => src.ProjectApplications));
        CreateMap<Project, ProjectModel>()
            .ReverseMap()
            .ForMember(dest => dest.ProjectApplications, opt => opt.MapFrom(src => src.Applications));
        CreateMap<ProjectModel, GetUserProjectModel>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Project, GetClientProjectModel>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // ProjectApplication mappings
        CreateMap<ApplyToProjectModel, ProjectApplication>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UpdateApplicationModel, ProjectApplication>();
        CreateMap<UpdateApplicationModel, UserAppliedProject>().ReverseMap();
        CreateMap<ProjectApplication, GetUserAppliedProjectModel>()
            // .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant))
            .ReverseMap();
        CreateMap<ProjectApplication, ProjectApplicationModel>().ReverseMap();
        // CreateMap<ProjectApplication, GetProjectApplicationModel>()
        //     .ReverseMap()
        //     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // UserSkills mappings
        CreateMap<UserSkills, UserSkillsModel>()
            .ReverseMap();

        CreateMap<Skill, GetSkillsModel>().ReverseMap();

        CreateMap<UserSocialLink, UserSocialLinkModel>().ReverseMap();

        CreateMap<Category, GetCategoriesModel>().ReverseMap();
        // CreateMap<Requirement, GetRequirementModel>().ReverseMap();
        CreateMap<ProjectRequirement, ProjectRequirementModel>()
            .ReverseMap();

        CreateMap<RequirementModel, Requirement>().ReverseMap();
        CreateMap<RequirementTest, GetRequirementTestModel>().ReverseMap();

        CreateMap<ProjectImageModel, ProjectImage>().ReverseMap();
        CreateMap<ProjectLink, ProjectLinkModel>().ReverseMap();
        CreateMap<ProjectApplication, GetProjectApplicationModel>().ReverseMap();

        CreateMap<Transaction, GetTransactionModel>().ReverseMap();
        CreateMap<Transaction, CreateTransactionCommand>().ReverseMap();

        //
    }
}