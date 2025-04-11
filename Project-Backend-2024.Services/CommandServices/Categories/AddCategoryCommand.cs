using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Categories;

public class AddCategoryCommand(
    IUnitOfWork unitOfWork,
    ICategoryRepository categoryRepository
)
    : IRequestHandler<AddCategoryModel, Unit>
{
    public async Task<Unit> Handle(AddCategoryModel request, CancellationToken cancellationToken)
    {
        categoryRepository.Insert(new Category() { Name = request.Name });

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}