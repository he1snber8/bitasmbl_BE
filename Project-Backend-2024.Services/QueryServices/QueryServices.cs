using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Services.QueryServices;

public class ProjectQueryService : BaseQueryService<Project, ProjectModel, IProjectRepository>, IProjectQueryService
{
    public ProjectQueryService(IUnitOfWork unitOfWork, IMapper mapper, IProjectRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class AppliedProjectQueryService : BaseQueryService<AppliedProject, AppliedProjectModel, IAppliedProjectRepository>, IAppliedProjectQueryService
{
    public AppliedProjectQueryService(IUnitOfWork unitOfWork, IMapper mapper, IAppliedProjectRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class SkillQueryService : BaseQueryService<Skill, SkillModel, ISkillRepository>, ISkillQueryService
{
    public SkillQueryService(IUnitOfWork unitOfWork, IMapper mapper, ISkillRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class UserSkillsQueryService : BaseQueryService<UserSkills, UserSkillsModel, IUserSkillsRepository>, IUserSkillsQueryService
{
    public UserSkillsQueryService(IUnitOfWork unitOfWork, IMapper mapper, IUserSkillsRepository repository) : base(unitOfWork, mapper, repository) { }
}
