using MediatR;

namespace Project_Backend_2024.Facade.FetchModels;

public class GetRequirementTestModel 
{
    public string Question { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }
}