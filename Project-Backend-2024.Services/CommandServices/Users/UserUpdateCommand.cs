using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Interfaces;
using Project_Backend_2024.Services.CommandServices.AWS;

namespace Project_Backend_2024.Services.CommandServices.Users;

public class UserUpdateCommand(
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork,
    ITeamManagerRepository userRepository,
    IMapper mapper,
    S3BucketService s3BucketService,
    UserManager<User> userManager) : IRequestHandler<UserUpdateModel, Unit>
{
    public async Task<Unit> Handle(UserUpdateModel request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                     ?? throw new ArgumentNullException("Could not validate user");

        var user = await userRepository.Set(u => u.Id == userId)
                       .Include(u => u.UserSocials)
                       .FirstOrDefaultAsync(cancellationToken)
                   ?? throw new UserNotFoundException();

        if (!string.IsNullOrEmpty(request.UserName))
            user.UserName = request.UserName;

        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            user.ImageUrl = request.ImageUrl;
            // var stream = await S3BucketService.FetchImageFromUrlAsync(request.ImageUrl);k

            // var bucketName = "my-bucket-bitasmbl";
            // var sanitizedImageUrl = Uri.EscapeDataString(request.ImageUrl);
            var key = $"profile-images/{user.UserName}";

            await s3BucketService.UploadImageFromUrlToS3Async(request.ImageUrl, key);
        }

        // if (request.Balance > 0)
        // {
        //     user.Balance += request.Balance;
        // }

        if (!string.IsNullOrEmpty(request.Bio))
            user.Bio = request.Bio;

        if (!string.IsNullOrEmpty(request.Password))
        {
            var hashedPassword = userManager.PasswordHasher.HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;
        }

        if (request.UserSocials != null)
        {
            
            // Convert existing links to a HashSet for fast lookup
            var existingSocialLinks = new HashSet<string>(user.UserSocials.Select(x => x.SocialUrl));
            var mappedSocialLinks = mapper.Map<List<UserSocialLink?>>(request.UserSocials);

            // Process incoming updates
            foreach (var mappedLink in mappedSocialLinks)
            {
                if (string.IsNullOrEmpty(mappedLink.SocialUrl) && existingSocialLinks.Contains(mappedLink.SocialUrl))
                {
                    // Remove from DB if frontend sends an empty string for an existing link
                    user.UserSocials.RemoveAll(x => x.SocialUrl == mappedLink.SocialUrl);
                }
                else if (!string.IsNullOrEmpty(mappedLink.SocialUrl) && !existingSocialLinks.Contains(mappedLink.SocialUrl))
                {
                    // Add new link if it's not in the DB
                    user.UserSocials.Add(mappedLink);
                }
            }
            

// Clean up: If a link exists in the DB but is not in mappedSocialLinks at all, remove it
            user.UserSocials.RemoveAll(existing => !mappedSocialLinks.Any(mapped => mapped.SocialUrl == existing.SocialUrl));   
        }


        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                // Handle specific error codes
                switch (error.Code)
                {
                    case "DuplicateUserName":
                        throw new Exception("The username is already taken.");
                    case "InvalidUserName":
                        throw new Exception("The username is invalid.");
                    case "PasswordTooWeak":
                        throw new PasswordValidationException(
                            "The provided password does not meet security requirements.");
                    case "ConcurrencyFailure":
                        throw new Exception("The user record has been modified by another process.");
                    default:
                        throw new Exception($"Update failed: {error.Description}");
                }
            }
        }

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}