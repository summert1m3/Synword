using Application.Users.DTOs;
using Ardalis.GuardClauses;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Specifications;
using Synword.Infrastructure.Services.Google;

namespace Application.Users.Services;

public class UserService : IUserService
{
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IGoogleApi _googleApi;
    private readonly ITokenClaimsService _tokenClaimsService;

    public UserService(IGoogleApi googleApi, 
        ISynwordRepository<User> userRepository, 
        ITokenClaimsService tokenClaimsService
        )
    {
        _userRepository = userRepository;
        _googleApi = googleApi;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<UserAuthenticateDTO> Authenticate(
        string accessToken, CancellationToken cancellationToken)
    {
        GoogleUserModel googleUserModel = 
            await _googleApi.GetGoogleUserData(accessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? user = await _userRepository.GetBySpecAsync(userSpec, cancellationToken);

        Guard.Against.Null(user, nameof(user));
        
        string token = await _tokenClaimsService.GenerateAccessToken(user.Id);

        return new UserAuthenticateDTO {Token = token};
    }
}
