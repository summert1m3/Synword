using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Google;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Domain.Enums;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Specifications;
using Synword.Infrastructure.Identity;

namespace Application.Guests.Commands;

public class RegisterViaGoogleSignInCommand : IRequest
{
    public RegisterViaGoogleSignInCommand(string accessToken, string uId)
    {
        AccessToken = accessToken;
        UId = uId;
    }
    
    public string AccessToken { get; }
    public string UId { get; }
}

internal class RegisterViaGoogleSignInCommandHandler : 
    IRequestHandler<RegisterViaGoogleSignInCommand>
{
    private readonly ISynwordRepository<User> _userRepository;
    private readonly IGoogleApi _googleApi;
    private readonly UserManager<AppUser> _userManager;

    public RegisterViaGoogleSignInCommandHandler(ISynwordRepository<User> userRepository,
        IGoogleApi googleApi, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _googleApi = googleApi;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(
        RegisterViaGoogleSignInCommand request, 
        CancellationToken cancellationToken)
    {
        GoogleUserModel googleUserModel = 
            await _googleApi.GetGoogleUserData(request.AccessToken);

        if (await IsUserAlreadySignedIn(googleUserModel, cancellationToken))
        {
            throw new AppValidationException("UserAlreadySignedIn");
        }

        User? user = 
            await _userRepository.GetByIdAsync(request.UId, cancellationToken);

        Guard.Against.Null(user, nameof(user));

        ExternalSignIn externalSignIn = new(ExternalSignInType.Google, googleUserModel.Id);
        
        user.AddExternalSignIn(externalSignIn);
        
        AppUser identityUser = await _userManager.FindByIdAsync(user.Id);

        await ChangeRole(identityUser);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<bool> IsUserAlreadySignedIn(
        GoogleUserModel googleUserModel,
        CancellationToken cancellationToken)
    {
        var spec = new UserByExternalIdSpecification(googleUserModel.Id);
        User? userBySpec = 
            await _userRepository.GetBySpecAsync(spec, cancellationToken);
        
        if (userBySpec is not null)
        {
            return true;
        }

        return false;
    }
    
    private async Task ChangeRole(AppUser identityUser)
    {
        await _userManager.RemoveFromRoleAsync(identityUser, Role.Guest.ToString());
        await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());
    }
}
