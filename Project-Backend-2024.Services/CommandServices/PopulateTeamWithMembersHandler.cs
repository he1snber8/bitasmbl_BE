using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Interfaces;
using SendGrid.Helpers.Errors.Model;

namespace Project_Backend_2024.Services.CommandServices;

public class PopulateTeamWithMembersHandler(ITeamRepository teamRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<PopulateTeamWithMembersCommand, Unit>
{


    public async Task<Unit> Handle(PopulateTeamWithMembersCommand request, CancellationToken cancellationToken)
    {
        var team = await teamRepository.Set(t => t.Id == request.TeamId)
                       .FirstOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken)
                   ?? throw new NotFoundException();
        
        var members = mapper.Map<List<TeamMember>>(request.Members);

        foreach (var user in members)
        {
            team.Members.Add(user);
        }

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;

    }
}