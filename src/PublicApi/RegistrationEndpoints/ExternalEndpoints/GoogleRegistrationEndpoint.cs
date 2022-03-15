using System.Security.Claims;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Extensions;
using MinimalApi.Endpoint;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;
using Synword.ApplicationCore.Enums;
using Synword.ApplicationCore.Interfaces;
using Synword.ApplicationCore.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Synword.PublicApi.RegistrationEndpoints.ExternalEndpoints;

public class GoogleRegistrationEndpoint : IEndpoint<IResult, GoogleRegistrationRequest>
{
    private IRepository<User>? _userRepository;
    private IGoogleApi? _googleApi;
    private HttpContext? _context;
    private UserManager<AppUser>? _userManager;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/googleRegister",
            [Authorize(Roles = nameof(Role.Guest))]
            async (GoogleRegistrationRequest request, IRepository<User> userRepository,
                IGoogleApi googleApi, HttpContext context, UserManager<AppUser> userManager) =>
            {
                _userRepository = userRepository;
                _googleApi = googleApi;
                _context = context;
                _userManager = userManager;
                return await HandleAsync(request);
            });
    }

    public async Task<IResult> HandleAsync(GoogleRegistrationRequest request)
    {
        Guard.Against.Null(request.AccessToken, nameof(request.AccessToken));
        Guard.Against.Null(_userRepository, nameof(_userRepository));
        Guard.Against.Null(_googleApi, nameof(_googleApi));
        Guard.Against.Null(_context, nameof(_context));

        GoogleUserModel googleUserModel = _googleApi.GetGoogleUserData(request.AccessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? userBySpec = await _userRepository.GetBySpecAsync(userSpec);
        
        if (userBySpec != null)
        {
            throw new Exception("User already exists");
        }
        
        string userId = _context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        User? user = await _userRepository.GetByIdAsync(userId);

        Guard.Against.Null(user, nameof(user));

        ExternalSignIn externalSignIn = new(ExternalSignInType.Google, googleUserModel.Id);
        
        user.AddExternalSignIn(externalSignIn);
        
        AppUser identityUser = await _userManager.FindByIdAsync(user.Id);
        await _userManager.RemoveFromRoleAsync(identityUser, Role.Guest.ToString());
        await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());

        await _userRepository.SaveChangesAsync();
        
        return Results.Ok("Google account is linked to synword account");
    }
}
