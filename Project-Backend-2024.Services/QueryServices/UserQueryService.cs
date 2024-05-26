using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Services.QueryServices;

//public class UserQueryService : BaseQueryService<User, UserModel, IUserRepository>, IUserQueryService
//{
//    public UserQueryService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository repository) : base(unitOfWork, mapper, repository) { }

//    public async Task<User?> RetrieveUserAsync(string id) => await Task.FromResult(_repository.Set(u => u.Id == id).FirstOrDefault());
//}
