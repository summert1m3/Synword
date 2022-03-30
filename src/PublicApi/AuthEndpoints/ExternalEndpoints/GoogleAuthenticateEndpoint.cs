using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Interfaces;
using Synword.ApplicationCore.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class GoogleAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GoogleAuthenticateRequest>
    .WithActionResult<GoogleAuthenticateResponse>
{
    private readonly IRepository<User>? _userRepository;
    private readonly IGoogleApi? _googleApi;
    private readonly ITokenClaimsService? _tokenClaimsService;

    public GoogleAuthenticateEndpoint(IGoogleApi googleApi, 
        IRepository<User> userRepository, ITokenClaimsService tokenClaimsService,
        UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _googleApi = googleApi;
        _tokenClaimsService = tokenClaimsService;
    }
    
    [HttpPost("api/googleAuthenticate")]
    public override async Task<ActionResult<GoogleAuthenticateResponse>> HandleAsync(
        GoogleAuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        GoogleUserModel googleUserModel = _googleApi.GetGoogleUserData(request.AccessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? user = await _userRepository.GetBySpecAsync(userSpec, cancellationToken);

        string token = await _tokenClaimsService!.GetTokenAsync(user.Id);
        
        return Ok(new GoogleAuthenticateResponse(token));
    }
}
