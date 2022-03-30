using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;
using Synword.ApplicationCore.Enums;
using Synword.ApplicationCore.Interfaces;
using Synword.ApplicationCore.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Synword.PublicApi.RegistrationEndpoints.ExternalEndpoints;

public class GoogleRegistrationEndpoint : EndpointBaseAsync
    .WithRequest<GoogleRegistrationRequest>
    .WithActionResult
{
    private readonly IRepository<User>? _userRepository;
    private readonly IGoogleApi? _googleApi;
    private readonly UserManager<AppUser>? _userManager;

    public GoogleRegistrationEndpoint(IRepository<User> userRepository,
        IGoogleApi googleApi, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _googleApi = googleApi;
        _userManager = userManager;
    }
    
    [HttpPost("api/googleRegister")]
    [Authorize(Roles = nameof(Role.Guest))]
    public override async Task<ActionResult> HandleAsync(
        GoogleRegistrationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        GoogleUserModel googleUserModel = _googleApi.GetGoogleUserData(request.AccessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? userBySpec = await _userRepository.GetBySpecAsync(userSpec, cancellationToken);
        
        if (userBySpec != null)
        {
            return BadRequest("User already exists");
        }
        
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        User? user = await _userRepository.GetByIdAsync(userId);

        Guard.Against.Null(user, nameof(user));

        ExternalSignIn externalSignIn = new(ExternalSignInType.Google, googleUserModel.Id);
        
        user.AddExternalSignIn(externalSignIn);
        
        AppUser identityUser = await _userManager.FindByIdAsync(user.Id);
        await _userManager.RemoveFromRoleAsync(identityUser, Role.Guest.ToString());
        await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());

        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return Ok("Google account is linked to Synword account");
    }
}
