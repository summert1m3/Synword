using Application.Users.DTOs;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.Identity.Token;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser>? _userManager;
    private readonly AppIdentityDbContext _identityDb;
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IGoogleApi _googleApi;
    private readonly ITokenClaimsService _tokenClaimsService;

    public UserService(
        UserManager<AppUser> userManager,
        AppIdentityDbContext identityDb,
        IGoogleApi googleApi, 
        ISynwordRepository<User> userRepository, 
        ITokenClaimsService tokenClaimsService
        )
    {
        _userManager = userManager;
        _identityDb = identityDb;
        _userRepository = userRepository;
        _googleApi = googleApi;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<UserAuthenticateDTO> Authenticate(
        string googleAccessToken, CancellationToken cancellationToken)
    {
        User user = await GetUserByGoogleAccessToken(
            googleAccessToken,
            cancellationToken);

        AppUser? userIdentity = await _userManager!.FindByIdAsync(user.Id);
        Guard.Against.Null(userIdentity);
        
        string accessToken = await _tokenClaimsService!.GenerateAccessToken(user.Id);

        RefreshToken refreshToken = await _tokenClaimsService.GenerateRefreshToken(
            user.Id, 
            Guid.NewGuid().ToString());

        userIdentity.AddRefreshToken(refreshToken);

        _identityDb.Update(userIdentity);
        
        await _identityDb.SaveChangesAsync(cancellationToken);

        return new UserAuthenticateDTO(accessToken, refreshToken.Token);
    }

    private async Task<User> GetUserByGoogleAccessToken(
        string googleAccessToken,
        CancellationToken cancellationToken = default)
    {
        GoogleUserModel googleUserModel = 
            await _googleApi.GetGoogleUserData(googleAccessToken);
        
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        
        User? user = await _userRepository.GetBySpecAsync(userSpec, cancellationToken);

        Guard.Against.Null(user, nameof(user));

        return user;
    }
}
