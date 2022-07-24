using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Application.Exceptions;
using Synword.Application.Guests.DTOs;
using Synword.Application.Interfaces.Google;
using Synword.Application.Interfaces.Identity.UserIdentity;
using Synword.Application.Users.DTOs;
using Synword.Infrastructure.Identity.Token;
using Synword.Persistence.Entities.Identity;
using Synword.Persistence.Entities.Identity.Token;
using Synword.Persistence.Identity;

namespace Synword.Infrastructure.Identity.UserIdentityServices;

public class UserAuthService : IUserAuthService
{
    private readonly AppIdentityDbContext _db;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly IJwtService _tokenClaimsService;
    private readonly IPasswordHasher<UserIdentity> _passwordHasher;
    private readonly AppIdentityDbContext _identityDb;
    private readonly IGoogleApi _googleApi;

    public UserAuthService(
        AppIdentityDbContext db,
        UserManager<UserIdentity> userManager,
        IJwtService tokenClaimsService,
        IPasswordHasher<UserIdentity> passwordHasher,
        AppIdentityDbContext identityDb,
        IGoogleApi googleApi)
    {
        _db = db;
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
        _passwordHasher = passwordHasher;
        _identityDb = identityDb;
        _googleApi = googleApi;
    }
    
    public async Task<GuestAuthenticateDto> GuestAuth(
        string uId,
        CancellationToken cancellationToken = default)
    {
        UserIdentity? guest = await _userManager!.FindByIdAsync(uId);

        Guard.Against.Null(guest);
        
        string accessToken = _tokenClaimsService!.GenerateAccessToken(guest.Id);

        RefreshToken refreshToken = _tokenClaimsService.GenerateRefreshToken(
            guest.Id, 
            Guid.NewGuid().ToString());
        
        guest.AddRefreshToken(refreshToken);

        _db.Update(guest);
        
        await _db.SaveChangesAsync(cancellationToken);

        return new GuestAuthenticateDto(accessToken, refreshToken.Token);
    }

    public async Task<UserAuthenticateDto> AuthViaEmail(
        string email, 
        string password, 
        CancellationToken cancellationToken = default)
    {
        UserIdentity userIdentity = await _userManager.FindByEmailAsync(email);

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

    public async Task<UserAuthenticateDto> AuthViaGoogle(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        GoogleUserModel googleUserModel = 
            await _googleApi.GetGoogleUserData(accessToken);
        
        UserIdentity userIdentity = await GetIdentityByGoogleId(
            googleUserModel,
            cancellationToken);

        UserAuthenticateDto tokens =
            await GenerateTokens(userIdentity, cancellationToken);

        return tokens;
    }
    
    private async Task<UserAuthenticateDto> GenerateTokens(
        UserIdentity userIdentity,
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
    
    private async Task<UserIdentity> GetIdentityByGoogleId(
        GoogleUserModel googleUserModel,
        CancellationToken cancellationToken = default)
    {
        UserIdentity userIdentity =
            _userManager.Users.SingleOrDefault(
                u => u.ExternalSignIn.Id == googleUserModel.Id);

        Guard.Against.Null(userIdentity, nameof(userIdentity));

        return userIdentity;
    }
}
