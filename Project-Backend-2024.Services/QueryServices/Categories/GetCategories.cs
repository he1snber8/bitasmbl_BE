using MediatR;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.QueryServices.Categories;

public class GetCategories(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesModel, List<Category>>
{
    public async Task<List<Category>> Handle(GetCategoriesModel request, CancellationToken cancellationToken) =>
        await categoryRepository.GetAll();
}