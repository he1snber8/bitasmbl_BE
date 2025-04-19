// using System.Security.Claims;
// using AutoMapper;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.DTO.Enums;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.AlterModels;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Facade.FetchModels;
// using Project_Backend_2024.Facade.GetModels;
// using Project_Backend_2024.Facade.Interfaces;
//
//
// namespace Project_Backend_2024.Services.CommandServices.ProjectApplications;
//
// public class ApplyToProjectCommand(
//     IUnitOfWork unitOfWork,
//     IProjectRepository projectRepository,
//     IMapper mapper,
//     IHttpContextAccessor httpHttpContextAccessor,
//     UserManager<User> userManager)
//     : IRequestHandler<ApplyToProjectModel, GetProjectApplicationModel>
// {
//     public async Task<GetProjectApplicationModel> Handle(ApplyToProjectModel request, CancellationToken cancellationToken)
//     {
//         if (request.ProjectId <= 0) throw new ArgumentException("Invalid Project ID", nameof(request.ProjectId));
//
//         var userId = httpHttpContextAccessor.HttpContext.User.Claims
//                          .FirstOrDefault(c => c.Type == "Id")?.Value ??
//                      throw new ArgumentException("user null");
//
//         var project = await projectRepository.Set(pr => pr.Id == request.ProjectId)
//                           .Include(pr => pr.ProjectApplications)
//                           .Include(pr => pr.ProjectRequirements)
//                           .FirstOrDefaultAsync(cancellationToken)
//                       ?? throw new ArgumentException("Project does not exist");
//
//         if (project.Status is not ProjectStatus.Active)
//             throw new InvalidProjectStatusException(project.Status);
//
//         if (project.ProjectApplications.FirstOrDefault(pa => pa.ApplicantId == userId) is not null)
//             throw new AlreadyAppliedException(project.Name);
//         
//         var projectRequirements = project.ProjectRequirements.Where(a => a.ProjectId == request.ProjectId).ToList();
//         
//         foreach (var projectRequirement in projectRequirements.Where(projectRequirement => request.RequirementIds.Contains(projectRequirement.RequirementId)))
//         {
//             if (projectRequirement.CurrentApplications + 1 > projectRequirement.MaxApplicationLimit)
//                 throw new Exception("Max application limit is reached, cannot apply");
//
//             projectRequirement.CurrentApplications += 1;
//         }
//         
//         if (projectRequirements.TrueForAll(pr => pr.CurrentApplications == pr.MaxApplicationLimit))
//         {
//             project.Status = ProjectStatus.Filled;
//         }
//
//         if (userId == project.PrincipalId)
//             throw new ArgumentException("Cannot apply to own project");
//         
//         var existingApplication = await projectRepository
//             .Set()
//             .Include(pr => pr.ProjectApplications) // Ensure ProjectApplications is included
//             .SelectMany(pr => pr.ProjectApplications)
//             .Where(a => a.ProjectId == request.ProjectId && a.ApplicantId == userId)
//             .FirstOrDefaultAsync(cancellationToken);
//
//
//         if (existingApplication != null)
//             throw new InvalidOperationException("User has already applied for this project");
//
//         var applicant = await userManager.FindByIdAsync(userId)
//                         ?? throw new ArgumentException("Could not retrieve applicant");
//
//         var appliedProject = new UserAppliedProject()
//         {
//             ProjectId = project.Id,
//             UserId = applicant.Id,
//             ApplicationStatus = ApplicationStatus.Pending,
//             DateApplied = DateTime.UtcNow,
//             CoverLetter = request.CoverLetter,
//             User = applicant,
//             Project = project,
//             QuizScore = (double) request.CorrectAnswers / request.TotalQuestions
//         };
//         
//         applicant.AppliedProjects.Add(appliedProject);
//
//         var principal = await userManager.FindByIdAsync(project.PrincipalId)
//                         ?? throw new ArgumentException("Could not retrieve principal");
//
//         if (string.IsNullOrEmpty(applicant.UserName) || string.IsNullOrEmpty(principal.UserName))
//             throw new ArgumentException("Username is empty");
//
//         project.Applications += 1;
//         
//         var application = mapper.Map<ProjectApplication>(request);
//
//         application.ApplicantId = userId;
//         application.PrincipalId = project.PrincipalId;
//         application.Applicant = applicant;
//         application.Project = project;
//         application.QuizScore = (double) request.CorrectAnswers / request.TotalQuestions;
//         
//
//         project.ProjectApplications.Add(application);
//
//         // unitOfWork.ProjectApplicationRepository.Insert(application);
//
//         await unitOfWork.SaveChangesAsync();
//
//         return mapper.Map<GetProjectApplicationModel>(application);
//     }
// }