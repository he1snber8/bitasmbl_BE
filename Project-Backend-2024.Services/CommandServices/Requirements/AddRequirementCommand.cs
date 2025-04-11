using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Requirements;

public class AddRequirementCommand(
    IUnitOfWork unitOfWork,
    IRequirementRepository requirementRepository
)
    : IRequestHandler<AddRequirementModel, Unit>
{
    public async Task<Unit> Handle(AddRequirementModel request, CancellationToken cancellationToken)
    {
        requirementRepository.Insert(new Requirement() { Name = request.Name });

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}