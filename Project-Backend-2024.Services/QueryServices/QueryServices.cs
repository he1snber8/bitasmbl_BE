using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Services.QueryServices;

public class UserQueryService : BaseQueryService<User, IUserRepository>, IUserQueryService
{
    public UserQueryService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class ProjectQueryService : BaseQueryService<Project, IProjectRepository>, IProjectQueryService
{
    public ProjectQueryService(IUnitOfWork unitOfWork, IMapper mapper, IProjectRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class AppliedProjectQueryService : BaseQueryService<AppliedProject, IAppliedProjectRepository>, IAppliedProjectQueryService
{
    public AppliedProjectQueryService(IUnitOfWork unitOfWork, IMapper mapper, IAppliedProjectRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class SkillQueryService : BaseQueryService<Skill, ISkillRepository>, ISkillQueryService
{
    public SkillQueryService(IUnitOfWork unitOfWork, IMapper mapper, ISkillRepository repository) : base(unitOfWork, mapper, repository) { }
}

public class UserSkillsQueryService : BaseQueryService<UserSkills, IUserSkillsRepository>, IUserSkillsQueryService
{
    public UserSkillsQueryService(IUnitOfWork unitOfWork, IMapper mapper, IUserSkillsRepository repository) : base(unitOfWork, mapper, repository) { }
}
