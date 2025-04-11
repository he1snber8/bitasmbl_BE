using MediatR;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.Exceptions;

namespace Project_Backend_2024.Services.Authentication.PasswordRecoveryAndReset;

public class PasswordResetHandler(UserManager<User> userManager) : IRequestHandler<PasswordResetModel, IdentityResult>
{
    public async Task<IdentityResult> Handle(PasswordResetModel request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Token))
            throw new ArgumentException("Could not retrieve access token");
        
        var user = await userManager.FindByEmailAsync(request.Email)
                   ?? throw new UserNotFoundException("User not found with the provided email.");
        
        var isTokenValid = await userManager.VerifyUserTokenAsync(
            user, 
            "Default",
            "ResetPassword", 
            request.Token 
        );
        
        if (isTokenValid is false)
            throw new ArgumentException("Invalid or expired token.");

        if (request.NewPassword != request.ConfirmPassword)
            throw new ArgumentException("New password and confirmation password do not match.");

        var result = await userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        return result;
    }
}