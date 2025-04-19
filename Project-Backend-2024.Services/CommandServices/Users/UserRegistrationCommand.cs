using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.FetchModels;
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
    public async Task<User> Register(RegisterUserModel registerModel)
    {
        var registrationType = registerModel.RegistrationType?.Trim().ToLower();

        // STEP 1: Check for existing user by Email
        var existingUser = await userManager.FindByEmailAsync(registerModel.Email);

        if (existingUser != null)
        {
            var roles = await userManager.GetRolesAsync(existingUser);
            if (!roles.Contains("User"))
                await userManager.AddToRoleAsync(existingUser, "User");

            // For non-team manager registrations, throw if same username is found
            if (registerModel.FirstName == existingUser.FirstName && registerModel.LastName == existingUser.LastName)
            {
                await userAuthentication.AuthenticateLogin(new UserLoginModel(
                    registerModel.Email,
                    // $"{registerModel.FirstName}_{registerModel.LastName}",
                    registerModel.Password,
                    registerModel.RegistrationType
                ));

                throw new EmailAlreadyExistsException(registerModel.Email, registerModel.RegistrationType);
            }
        }

        // STEP 2: Map to proper User subtype based on RegistrationType
        var user = registrationType switch
        {
            var type when type == RegistrationType.StandardTeamManager.ToString().ToLower() => mapper.Map<TeamManager>(
                registerModel),
            var type when type == RegistrationType.StandardOrganizationManager.ToString().ToLower() => mapper
                .Map<OrganizationManager>(registerModel),
            var type when type == RegistrationType.Google.ToString().ToLower() => mapper.Map<User>(registerModel),
            _ => throw new InvalidOperationException($"Unsupported registration type: {registerModel.RegistrationType}")
        };

        // STEP 3: Set password for standard user, or , if registered as Google account, set it as a random guid

        if (registrationType == RegistrationType.StandardTeamManager.ToString().ToLower() ||
            registrationType == RegistrationType.StandardOrganizationManager.ToString().ToLower())
        {
            if (registerModel.Password == null) throw new PasswordValidationException("Password must not be empty");

            user.PasswordHash = passwordHasher.HashPassword(user, registerModel.Password);
        }
        else
        {
            // var googleUser = (GoogleUser)registerModel;
            user.EmailConfirmed = true;
            // user.FirstName = user.f;
            // user.FirstName = (GoogleUser)user.las;
            // user.PasswordHash = passwordHasher.HashPassword(user, Guid.NewGuid().ToString());
        }

        // ✅ Ensure UserName is always set — Identity requires it
        if (string.IsNullOrWhiteSpace(user.UserName))
        {
            user.UserName = (registerModel.Email ?? $"{registerModel.FirstName}_{registerModel.LastName}")
                .Replace(" ", "_").ToLower();
        }


        // STEP 4: Create the user
        var isStandardRegistration =
            registrationType == RegistrationType.StandardTeamManager.ToString().ToLower() ||
            registrationType == RegistrationType.StandardOrganizationManager.ToString().ToLower();

        var creationResult = isStandardRegistration
            ? await userManager.CreateAsync(user, registerModel.Password)
            : await userManager.CreateAsync(user);

        if (!creationResult.Succeeded)
            HandleIdentityErrors(creationResult);

        // STEP 5: Assign to role and sign in
        await userManager.AddToRoleAsync(user, "User");
        await cookieGenerator.GenerateCookieAndSignIn(user);

        // STEP 6: Email confirmation for standard registration
        // if (registrationType == RegistrationType.StandardTeamManager.ToString().ToLower())
        // {
        //     await SendEmailConfirmationAsync(user, registerModel.Email);
        // }

        await unitOfWork.SaveChangesAsync();

        return user;
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
                    throw new DuplicateNameException();
                default:
                    throw new IdentityException(creationResult.Errors);
            }
        }
    }
}