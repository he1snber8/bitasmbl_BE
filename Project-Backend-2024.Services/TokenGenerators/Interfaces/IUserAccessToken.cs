using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.TokenGenerators.Interfaces
{
    public interface IUserAccessToken : IAccessToken<User> { }
}
