using System.Security.Claims;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Domain.Enums;
using Synword.Domain.Interfaces;
using Synword.Domain.Specifications;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;

namespace Application.Users.Commands;

public class RegisterNewGoogleUserCommand : IRequest
{
    public RegisterNewGoogleUserCommand(string accessToken, ClaimsPrincipal user)
    {
        AccessToken = accessToken;
        User = user;
    }
    
    public string? AccessToken { get; }
    public ClaimsPrincipal User { get; }
}

internal class RegisterNewGoogleUserCommandHandler : 
    IRequestHandler<RegisterNewGoogleUserCommand>
{
    private readonly IRepository<User>? _userRepository;
    private readonly IGoogleApi? _googleApi;
    private readonly UserManager<AppUser>? _userManager;

    public RegisterNewGoogleUserCommandHandler(IRepository<User> userRepository,
        IGoogleApi googleApi, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _googleApi = googleApi;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(RegisterNewGoogleUserCommand request, CancellationToken cancellationToken)
    {
        GoogleUserModel googleUserModel = await _googleApi.GetGoogleUserData(request.AccessToken);
        var userSpec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? userBySpec = await _userRepository.GetBySpecAsync(userSpec, cancellationToken);
        
        if (userBySpec != null)
        {
            throw new Exception("User already exists");
        }
        
        string userId = request.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        User? user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        Guard.Against.Null(user, nameof(user));

        ExternalSignIn externalSignIn = new(ExternalSignInType.Google, googleUserModel.Id);
        
        user.AddExternalSignIn(externalSignIn);
        
        AppUser identityUser = await _userManager.FindByIdAsync(user.Id);
        await _userManager.RemoveFromRoleAsync(identityUser, Role.Guest.ToString());
        await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
