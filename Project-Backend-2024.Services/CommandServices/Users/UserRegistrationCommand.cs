using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.Authentication.CookieGenerators;
using Project_Backend_2024.Services.CommandServices.AWS;
using Project_Backend_2024.Services.Formatting;

namespace Project_Backend_2024.Services.CommandServices.Users;

public class UserRegistrationCommand(
    IMapper mapper,
    UserManager<User> userManager,
    CookieGenerator cookieGenerator,
    IPasswordHasher<User> passwordHasher,
    IUserAuthentication userAuthentication,
    IUnitOfWork unitOfWork,
    IEmailService emailService) : IUserRegistration
{
    public async Task Register(RegisterUserModel registerModel)
    {
        // Check if the email is already registered
        var existingUser = registerModel.Email is null
            ? await userManager.FindByEmailAsync(registerModel.Username)
            : await userManager.FindByEmailAsync(registerModel.Email);

        if (existingUser != null)
        {
            var roles = await userManager.GetRolesAsync(existingUser);
            if (!roles.Contains("User"))
            {
                await userManager.AddToRoleAsync(existingUser, "User");
            }

            if (registerModel.RegistrationType?.ToLower() != RegistrationType.Standard.ToString().ToLower()
                && registerModel.Username == existingUser.UserName)
            {
                await userAuthentication.AuthenticateLogin(new UserLoginModel(
                    registerModel.Email,
                    null,
                    null,
                    registerModel.RegistrationType));

                throw new EmailAlreadyExistsException(registerModel.Email, registerModel.RegistrationType);
            }
        }


        // Validate email format if user isn't using github login
        if (registerModel.RegistrationType?.ToLower() != RegistrationType.Github.ToString().ToLower() &&
            !EmailFormatValidator.IsValidEmail(registerModel.Email))
            throw new EmailValidationException();

        // Map registration model to User entity
        var user = mapper.Map<User>(registerModel);
        user.DateJoined = DateTime.Now;

        if (registerModel.RegistrationType?.ToLower() == RegistrationType.Standard.ToString().ToLower())
        {
            // For standard registration, hash the provided password
            user.PasswordHash = passwordHasher.HashPassword(user, registerModel.Password);
        }
        else
        {
            // For SSO registrations, mark email as confirmed and set dummy password
            user.EmailConfirmed = true;
            user.PasswordHash = passwordHasher.HashPassword(user, Guid.NewGuid().ToString());
        }

        // Create user in the system
        IdentityResult creationResult;

        if (registerModel.RegistrationType?.ToLower() == RegistrationType.Standard.ToString().ToLower())
        {
            // Standard registration requires a password
            creationResult = await userManager.CreateAsync(user, registerModel.Password);
        }
        else
        {
            // SSO registration does not require a password
            
            user.UserName = user.UserName?.Replace(" ", "_");
            creationResult = await userManager.CreateAsync(user);
        }

        if (!creationResult.Succeeded)
        {
            HandleIdentityErrors(creationResult);
        }

        // Add user to default role and generate sign-in cookie
        await userManager.AddToRoleAsync(user, "User");
        await cookieGenerator.GenerateCookieAndSignIn(user);

        // Handle email confirmation for standard registration
        if (registerModel.RegistrationType?.ToLower() == RegistrationType.Standard.ToString().ToLower())
        {
            await SendEmailConfirmationAsync(user, registerModel.Email);
        }

        await unitOfWork.SaveChangesAsync();
    }

    private async Task SendEmailConfirmationAsync(User user, string email)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var callbackBaseUrl =
            Environment.GetEnvironmentVariable("CALLBACK_BASE_URL") ??
            "https://3e92-176-221-143-53.ngrok-free.app"; // Replace with a proper default value
        var callbackUrl =
            $"{callbackBaseUrl}/Users/confirm?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        var htmlContent = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";

        await emailService.SendEmailToSubject(email, callbackUrl, "Welcome!",
            "Finish mail confirmation below", htmlContent);
    }

    private void HandleIdentityErrors(IdentityResult creationResult)
    {
        var error = creationResult.Errors.FirstOrDefault();
        if (error is not null)
        {
            switch (error.Code)
            {
                case "PasswordTooShort":
                    throw new PasswordValidationException("Password is too short.");
                case "PasswordRequiresDigit":
                    throw new PasswordValidationException("Password must contain at least one digit.");
                case "PasswordRequiresLower":
                    throw new PasswordValidationException("Password must contain at least one lowercase letter.");
                case "PasswordRequiresUpper":
                    throw new PasswordValidationException("Password must contain at least one uppercase letter.");
                case "PasswordRequiresNonAlphanumeric":
                    throw new PasswordValidationException("Password must contain at least one symbol.");
                case "DuplicateUserName":
                    throw new EntityAlreadyExistsException(error.Description);
                default:
                    throw new IdentityException(creationResult.Errors);
            }
        }
    }
}