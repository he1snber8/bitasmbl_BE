using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.GetModels;

namespace Project_Backend_2024.Facade;

public record ApplySkillModel(string Name) : IRequest<Unit>;

public record AddSkillModel(string Name) : IRequest<Unit>;

public record RemoveSkillModel(int Id) : IRequest<Unit>;

public record AddRequirementModel(string Name) : IRequest<Unit>;

public record AddCategoryModel(string Name) : IRequest<Unit>;

public record PasswordRecoveryRequestModel(string Email) : IRequest<Unit>;

public record PasswordResetModel(string Email, string NewPassword, string ConfirmPassword, string Token)
    : IRequest<IdentityResult>;


public record UpdateApplicationModel(int Id, string? ApplicationStatus) : IRequest<GetProjectApplicationModel>;



