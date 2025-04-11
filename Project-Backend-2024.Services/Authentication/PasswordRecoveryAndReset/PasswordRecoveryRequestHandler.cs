using MediatR;
using Microsoft.AspNetCore.Identity;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.Authentication.PasswordRecoveryAndReset;


public class PasswordRecoveryRequestHandler(UserManager<User> userManager, IEmailService emailService)
    : IRequestHandler<PasswordRecoveryRequestModel, Unit>
{
    public async Task<Unit> Handle(PasswordRecoveryRequestModel request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email)
                   ?? throw new UserNotFoundException(request.Email);

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var baseUrl = "https://bitasmbl-front-latest.onrender.com";
            // Environment.GetEnvironmentVariable("CALLBACK_URL") 
            // ?? throw new InvalidOperationException("Environment variable 'CALLBACK_URL' is not set.");

        var callbackUrl = $"{baseUrl}/password-reset?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(request.Email)}";

        var htmlContent = $@"
        <html>
        <body>
            <h2>Password Recovery</h2>
            <p>Please click the link below to reset your password:</p>
            <a href='{callbackUrl}'>Reset Password</a>
            <p>If you did not request this, you can safely ignore this email.</p>
        </body>
        </html>";

        await emailService.SendEmailToSubject(request.Email, callbackUrl, "Password reset!",
            "reset password below", htmlContent);

        return Unit.Value;
    }
}