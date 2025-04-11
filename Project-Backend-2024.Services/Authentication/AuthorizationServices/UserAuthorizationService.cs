// using AutoMapper;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.Facade;
// using Project_Backend_2024.Facade.Exceptions;
// using Project_Backend_2024.Services.Authentication.CookieGenerators;
// using Project_Backend_2024.Facade.Interfaces;
// using Project_Backend_2024.Services.Formatting;
//
// namespace Project_Backend_2024.Services.Authentication.AuthorizationServices;
//
// public class UserAuthorizationService(
//     IMapper mapper,
//     UserManager<User> userManager,
//     CookieGenerator cookieGenerator,
//     IEmailService emailService,
//     IHttpContextAccessor httpContextAccessor) : IUserAuthorizationService
// {
//     public async Task<User> AuthenticateLogin(UserLoginModel loginModel)
//     {
//         if (!EmailFormatValidator.IsValidEmail(loginModel.Email))
//             throw new EmailValidationException();
//         
//         var user = await userManager.FindByEmailAsync(loginModel.Email)
//                    ?? throw new UserLoginException(loginModel.Email);
//         
//         if (await userManager.CheckPasswordAsync(user, loginModel.Password) is false)
//         {
//             await userManager.AccessFailedAsync(user);
//             throw new UserLoginException(user.Email, true);
//         }
//
//         if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
//             throw new UserLockedException(user.Email);
//
//         user.LastLogin = DateTime.Now;
//
//         await userManager.UpdateAsync(user);
//
//         await cookieGenerator.GenerateCookieAndSignIn(user);
//
//         return user;
//     }
//     public async Task Register(RegisterUserModel registerModel)
//     {
//         if (await userManager.FindByEmailAsync(registerModel.Email) is not null)
//             throw new EmailAlreadyExistsException(registerModel.Email);
//
//         if (!EmailFormatValidator.IsValidEmail(registerModel.Email))
//             throw new EmailValidationException();
//
//         var user = mapper.Map<User>(registerModel);
//
//         user.DateJoined = DateTime.Now;
//
//         var creationResult = await userManager.CreateAsync(user, registerModel.Password);
//
//         if (creationResult.Succeeded)
//         {
//             var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
//             
//             var callbackUrl =
//                 $"https://f526-176-221-143-53.ngrok-free.app/Users/confirm?userId={user.Id}&token={Uri.EscapeDataString(token)}";
//
//             var htmlContent = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";
//
//             await emailService.SendEmailConfirmationAsync(registerModel.Email, callbackUrl, "Welcome!",
//                 "finish mail confirmation below", htmlContent);
//             await userManager.AddToRoleAsync(user, "User");
//             await cookieGenerator.GenerateCookieAndSignIn(user);
//         }
//         else
//         {
//             var error = creationResult.Errors.FirstOrDefault();
//             if (error is not null)
//             {
//                 switch (error.Code)
//                 {
//                     case "PasswordTooShort":
//                         throw new PasswordValidationException("Password is too short.");
//                     case "PasswordRequiresDigit":
//                         throw new PasswordValidationException("Password must contain at least one digit.");
//                     case "PasswordRequiresLower":
//                         throw new PasswordValidationException("Password must contain at least one lowercase letter.");
//                     case "PasswordRequiresUpper":
//                         throw new PasswordValidationException("Password must contain at least one uppercase letter.");
//                     case "PasswordRequiresNonAlphanumeric":
//                         throw new PasswordValidationException("Password must contain at least one symbol.");
//                     case "DuplicateUserName":
//                         throw new EntityAlreadyExistsException(registerModel.Username);
//                     default:
//                         throw new IdentityException(creationResult.Errors);
//                 }
//             }
//         }
//     }
//
//     public async Task SignOutAsync()
//     {
//         await httpContextAccessor.HttpContext.SignOutAsync("Cookies");
//     }
// }