using AutoMapper;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.BasicOperations;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Interfaces.Commands;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.Services.CommandServices;

//public class UserCommandService/* : BaseCommandService<UserModel, User, IUserRepository>, IUserCommandService*/
//{
//    public UserCommandService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository repository) : base(unitOfWork, mapper, repository) { }

//    public /*override*/ async Task<int> Insert(UserModel model)
//    {
//        if (!model.ValidateUsername() || _repository.Set(m => m.UserName == model.Username).SingleOrDefault() != null) throw new UsernameValidationException("Username format is wrong or is already in use, please check again!");
//        if (!model.ValidatePassword()) throw new PasswordValidationException("Password format is incorrect, please check again!");
//        if (!model.ValidateEmail() || _repository.Set(m => m.Email == model.Email).SingleOrDefault() != null) throw new EmailValidationException("Email format is wrong or is already registered, please check again!");

//        return await base.Insert(model);
//    }

//    public async Task<RegisterUserModel> Register(RegisterUserModel registerModel)
//    {
//        var model = _mapper.Map<UserModel>(registerModel);

//        await Insert(model);

//        return registerModel;
//    }

//    public async Task<(bool,UserModel)> AutheticateLogin(IAuthenticatable loginModel)
//    {
//        User user = _repository.Set(m => m.UserName == loginModel.Username).SingleOrDefault() ?? throw new ArgumentNullException();

//        user.LastLogin = DateTime.Now;

//        await _unitOfWork.SaveChangesAsync();

//        var model = _mapper.Map<UserModel>(user);

//        return (loginModel.Password.HashEquals(model.Password), model);
//    }

//}
