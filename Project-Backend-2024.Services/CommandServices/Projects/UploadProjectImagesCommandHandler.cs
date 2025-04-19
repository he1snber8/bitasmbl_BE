// using MediatR;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.AlterModels;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Facade.Interfaces;
// using Project_Backend_2024.Services.CommandServices.AWS;
//
// namespace Project_Backend_2024.Services.CommandServices.Projects;
//
// public class UploadProjectImagesCommandHandler(
//     IUnitOfWork unitOfWork,
//     S3BucketService s3BucketService,
//     IProjectRepository projectRepository)
//     : IRequestHandler<UploadProjectImagesModel, Unit>
// {
//     public async Task<Unit> Handle(UploadProjectImagesModel? request, CancellationToken cancellationToken)
//     {
//         // if (request?.Files is null) return Unit.Value;
//
//         var project = projectRepository.Set(p => p.Id == request.ProjectId).SingleOrDefault()
//                       ?? throw new ProjectNotFoundException();
//
//         if (request.Files is null)
//         {
//             throw new ProjectMediaNotIncludedException();
//         }
//
//         var urls = await s3BucketService.UploadFilesToS3Async(request.Files);
//
//         project.ProjectImages = urls.Select(fileUrl => new ProjectImage()
//         {
//             ProjectId = request.ProjectId,
//             ImageUrl = fileUrl
//         }).ToList();
//
//         await unitOfWork.SaveChangesAsync();
//
//         return Unit.Value;
//     }
// }