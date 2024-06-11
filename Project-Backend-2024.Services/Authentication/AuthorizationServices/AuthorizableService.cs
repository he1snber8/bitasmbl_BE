using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.TokenGenerators;
using Project_Backend_2024.Services.TokenValidators;
using System.Security.Claims;

namespace Project_Backend_2024.Services.Authentication.Authorization;

public class AuthorizableService : IAuthorizableService
{
    private readonly IMapper _mapper;
    private readonly ILogger<AuthorizableService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly RefreshTokenGenerator _refreshTokenGenerator;
    private readonly TokenValidator _tokenValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizableService(IMapper mapper, 
        UserManager<User> userManager,
        AccessTokenGenerator accessTokenGenerator,
        RefreshTokenGenerator refreshTokenGenerator,
        TokenValidator tokenValidator,
        ILogger<AuthorizableService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _userManager = userManager;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenValidator = tokenValidator;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AuthenticateLogin(UserLoginModel loginModel)
    {
        var user = await _userManager.FindByNameAsync(loginModel.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            throw new UserLoginException();

        var refreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

        if (refreshToken is null || !_tokenValidator.IsRefreshTokenValid(refreshToken))
        {
            refreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);
        }

        await _accessTokenGenerator.GenerateAndSignIn(user);
    }

    public async Task RefreshToken(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken)) throw new Exception("Access token not found in cookies.");

        var userIdentity = _httpContextAccessor.HttpContext.User.Identity 
            ?? throw new ArgumentNullException("Could not retrieve user");

        if (userIdentity.IsAuthenticated)
        {
            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
            var userId = userClaims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (string.IsNullOrEmpty(userId)) throw new Exception("User ID not found in claims.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) throw new Exception("User not found.");

            var storedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

            if (storedRefreshToken is null || _tokenValidator.IsRefreshTokenValid(storedRefreshToken) is false)
            {
                var newRefreshToken = _refreshTokenGenerator.GenerateRefreshToken();
                await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);
            }

            await _accessTokenGenerator.GenerateAndSignIn(user);
            return;
        }

        throw new Exception("User is not authenticated.");
    }

    public async Task Register(RegisterUserModel registerModel)
    {
        if (await _userManager.FindByEmailAsync(registerModel.Email) != null)
            throw new EmailValidationException(registerModel.Email);

        var user = _mapper.Map<User>(registerModel);

        var result = await _userManager.CreateAsync(user, registerModel.Password);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        await _userManager.AddToRoleAsync(user, "User");

        await _accessTokenGenerator.GenerateAndSignIn(user);

        string refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

        await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);
    }
}
