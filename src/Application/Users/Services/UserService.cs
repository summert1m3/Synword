using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Google;
using Application.Interfaces.Services.Token;
using Application.Users.DTOs;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.Identity.Token;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Specifications;
using Synword.Infrastructure.Identity;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppIdentityDbContext _identityDb;
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IGoogleApi _googleApi;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public UserService(
        UserManager<AppUser> userManager,
        AppIdentityDbContext identityDb,
        IGoogleApi googleApi, 
        ISynwordRepository<User> userRepository, 
        ITokenClaimsService tokenClaimsService,
        IPasswordHasher<AppUser> passwordHasher
        )
    {
        _userManager = userManager;
        _identityDb = identityDb;
        _userRepository = userRepository;
        _googleApi = googleApi;
        _tokenClaimsService = tokenClaimsService;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<UserAuthenticateDto> AuthViaGoogleSignIn(
        string googleAccessToken, 
        CancellationToken cancellationToken = default)
    {
        User user = await GetUserByGoogleAccessToken(
            googleAccessToken,
            cancellationToken);

        AppUser? userIdentity = await _userManager!.FindByIdAsync(user.Id);
        Guard.Against.Null(userIdentity);
        
        UserAuthenticateDto tokens =
            await GenerateTokens(userIdentity, cancellationToken);

        return tokens;
    }

    public async Task<UserAuthenticateDto> AuthViaEmail(
        string email, 
        string password, 
        CancellationToken cancellationToken = default)
    {
        AppUser userIdentity = await _userManager.FindByEmailAsync(email);

        if (userIdentity is null)
        {
            throw new AppValidationException("Invalid email or password");
        }

        if (!userIdentity.EmailConfirmed)
        {
            throw new AppValidationException("Email not confirmed");
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(
            userIdentity,
            userIdentity.PasswordHash,
            password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            throw new AppValidationException("Invalid email or password");
        }
        
        UserAuthenticateDto tokens =
            await GenerateTokens(userIdentity, cancellationToken);

        return tokens;
    }

    private async Task<User> GetUserByGoogleAccessToken(
        string googleAccessToken,
        CancellationToken cancellationToken = default)
    {
        GoogleUserModel googleUserModel = 
            await _googleApi.GetGoogleUserData(googleAccessToken);
        
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        
        User? user = 
            await _userRepository.GetBySpecAsync(userSpec, cancellationToken);

        Guard.Against.Null(user, nameof(user));

        return user;
    }

    private async Task<UserAuthenticateDto> GenerateTokens(
        AppUser userIdentity,
        CancellationToken cancellationToken = default)
    {
        string accessToken = 
            _tokenClaimsService.GenerateAccessToken(userIdentity.Id);

        RefreshToken refreshToken = _tokenClaimsService.GenerateRefreshToken(
            userIdentity.Id, 
            Guid.NewGuid().ToString());

        userIdentity.AddRefreshToken(refreshToken);

        _identityDb.Update(userIdentity);
        
        await _identityDb.SaveChangesAsync(cancellationToken);

        return new UserAuthenticateDto(accessToken, refreshToken.Token);
    }
}
