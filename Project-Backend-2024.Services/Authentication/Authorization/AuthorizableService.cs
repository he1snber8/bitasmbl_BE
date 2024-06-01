using AutoMapper;
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
    private readonly PrincipalTokenValidator _refreshTokenValidator;

    public AuthorizableService(IMapper mapper, UserManager<User> userManager,
        AccessTokenGenerator accessTokenGenerator,
        RefreshTokenGenerator refreshTokenGenerator,
        PrincipalTokenValidator refreshTokenValidator,
        ILogger<AuthorizableService> logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenValidator = refreshTokenValidator;
        _logger = logger;
    }

    public async Task AuthenticateLogin(UserLoginModel loginModel,HttpContext httpContext)
    {
        var user = await _userManager.FindByNameAsync(loginModel.Username);

        if (user is null || await _userManager.CheckPasswordAsync(user, loginModel.Password) is false)
            throw new UserLoginException();
        
        _accessTokenGenerator.SetAccessTokenInCookie(user,httpContext);

        var refreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

        if (refreshToken == null)
        {
             _refreshTokenGenerator.SetRefreshTokenInCookie(httpContext);
            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);
        }

        _refreshTokenGenerator.SetRefreshTokenInCookie(httpContext,refreshToken);
    }

    public async Task RefreshToken(string expiredAccessToken,string refreshToken, HttpContext httpContext)
    {
        var principal = await GetPrincipalFromExpiredTokenAsync(expiredAccessToken);

        if (principal?.Identity?.Name is null)
            throw new UnauthorizedAccessException();

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);
        if (user is null) throw new UserLoginException();

        var storedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

        if (storedRefreshToken != refreshToken) throw new TokenValidationException("Token validation failed.");

        _accessTokenGenerator.SetAccessTokenInCookie(user,httpContext);

    }

    private async Task<ClaimsPrincipal> GetPrincipalFromExpiredTokenAsync(string token)
    {
        try
        {
            return await Task.FromResult(_refreshTokenValidator.GetPrincipalFromToken(token));
        }
        catch (SecurityTokenExpiredException ex)
        {
            _logger.LogWarning(ex, "Token expired.");
            throw new TokenValidationException("Token has expired.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token validation failed.");
            throw new TokenValidationException("Token validation failed.");
        }
    }

    public async Task Register(RegisterUserModel registerModel, HttpContext httpContext)
    {
        if (await _userManager.FindByEmailAsync(registerModel.Email) != null)
            throw new EmailValidationException(registerModel.Email);

        var user = _mapper.Map<User>(registerModel);

        var result = await _userManager.CreateAsync(user, registerModel.Password);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        await _userManager.AddToRoleAsync(user, "User");

        try
        {
             _accessTokenGenerator.SetAccessTokenInCookie(user,httpContext);
             string refreshToken = _refreshTokenGenerator.SetRefreshTokenInCookie(httpContext);

            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);
        }
        catch (Exception)
        {
            throw;
        }

    }
}
