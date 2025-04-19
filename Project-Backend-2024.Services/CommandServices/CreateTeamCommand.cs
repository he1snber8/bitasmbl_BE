using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices;

public class InitializeTeamCommandHandler(
    ITeamRepository teamRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<InitializeTeamCommand, Unit>
{
    public async Task<Unit> Handle(InitializeTeamCommand request, CancellationToken cancellationToken)
    {
        var team = mapper.Map<Team>(request);
        
        teamRepository.Insert(team);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }

    // Stub for file upload logic
    private async Task<string> UploadFileAsync(IFormFile file)
    {
        // Replace with real logic (e.g., Azure Blob, local disk, etc.)
        var filePath = Path.Combine("uploads", Guid.NewGuid() + Path.GetExtension(file.FileName));
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return filePath;
    }
}