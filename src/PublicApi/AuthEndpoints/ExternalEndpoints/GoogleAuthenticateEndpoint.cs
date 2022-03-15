using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;
using Synword.ApplicationCore.Interfaces;
using Synword.ApplicationCore.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class GoogleAuthenticateEndpoint : IEndpoint
{
    private IRepository<User>? _userRepository;
    private IGoogleApi? _googleApi;
    private ITokenClaimsService? _tokenClaimsService;
    private UserManager<AppUser>? _userManager;
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/googleAuthenticate",
            async (GoogleAuthenticateRequest request, IGoogleApi googleApi, 
                IRepository<User> userRepository, ITokenClaimsService tokenClaimsService,
                UserManager<AppUser> userManager) =>
            {
                _userRepository = userRepository;
                _googleApi = googleApi;
                _tokenClaimsService = tokenClaimsService;
                _userManager = userManager;
                return await HandleAsync(request);
            });
    }

    public async Task<IResult> HandleAsync(GoogleAuthenticateRequest request)
    {
        GoogleUserModel googleUserModel = _googleApi.GetGoogleUserData(request.AccessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? user = await _userRepository.GetBySpecAsync(userSpec);

        string token = await _tokenClaimsService!.GetTokenAsync(user.Id);
        
        return Results.Ok(new GoogleAuthenticateResponse(token));
    }
}
