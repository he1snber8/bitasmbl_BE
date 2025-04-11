using Amazon.Runtime.Internal;
using MediatR;

namespace Project_Backend_2024.Facade.FetchModels;

public class GetSkillsModel : IRequest<List<GetSkillsModel>>
{
    public required string Name { get; set; }
}