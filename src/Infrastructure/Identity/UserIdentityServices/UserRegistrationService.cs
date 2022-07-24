using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Application.Exceptions;
using Synword.Application.Interfaces.Google;
using Synword.Application.Interfaces.Identity.UserIdentity;
using Synword.Domain.Enums;
using Synword.Persistence.Entities.Identity;
using Synword.Persistence.Entities.Identity.ValueObjects;

namespace Synword.Infrastructure.Identity.UserIdentityServices;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly UserManager<UserIdentity> _userManager;
    private readonly IPasswordHasher<UserIdentity> _passwordHasher;
    private readonly IGoogleApi _googleApi;

    public UserRegistrationService(
        UserManager<UserIdentity> userManager,
        IPasswordHasher<UserIdentity> passwordHasher,
        IGoogleApi googleApi)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _googleApi = googleApi;
    }
    
    public async Task RegisterNewGuest(string uId)
    {
        UserIdentity guest = new()
        {
            Id = uId, 
            UserName = uId
        };

        await _userManager!.CreateAsync(guest);

        await _userManager.AddToRoleAsync(guest, Role.Guest.ToString());
    }

    public async Task RegisterGuestViaEmail(
        string uId, string email, string password)
    {
        UserIdentity? identityUser = 
            await _userManager!.FindByIdAsync(uId);
        Guard.Against.Null(identityUser);
        
        if (await IsUserAlreadyRegistered(identityUser))
        {
            throw new AppValidationException("UserAlreadyRegistered");
        }
        
        if (await IsUserRegisteredWithSameEmail(email))
        {
            throw new AppValidationException("UserRegisteredWithSameEmail");
        }

        identityUser.Email = email;
        identityUser.PasswordHash = 
            _passwordHasher.HashPassword(identityUser, password);
        identityUser.SecurityStamp = Guid.NewGuid().ToString();

        await ChangeRole(identityUser);

        await _userManager.UpdateAsync(identityUser);
    }

    public async Task RegisterGuestViaGoogle(string uId, string accessToken)
    {
        GoogleUserModel googleUserModel = 
            await _googleApi.GetGoogleUserData(accessToken);

        if (await IsUserRegisteredWithSameGoogleId(googleUserModel))
        {
            throw new AppValidationException("UserAlreadySignedIn");
        }
        
        UserIdentity? identityUser = 
            await _userManager!.FindByIdAsync(uId);
        Guard.Against.Null(identityUser);
        
        if (await IsUserAlreadyRegistered(identityUser))
        {
            throw new AppValidationException("UserAlreadyRegistered");
        }

        ExternalSignIn externalSignIn = new(ExternalSignInType.Google, googleUserModel.Id);
        
        identityUser.AddExternalSignIn(externalSignIn);

        await _userManager.UpdateAsync(identityUser);

        await ChangeRole(identityUser);
    }

    private async Task<bool> IsUserAlreadyRegistered(UserIdentity identityUser)
    {
        var roles = await _userManager.GetRolesAsync(identityUser);

        var role = roles.SingleOrDefault(r => r == nameof(Role.Guest));

        return role is null;
    }
    
    private async Task<bool> IsUserRegisteredWithSameEmail(string email)
    {
        UserIdentity userIdentity = await _userManager.FindByEmailAsync(email);

        return userIdentity is not null;
    }
    
    private async Task<bool> IsUserRegisteredWithSameGoogleId(
        GoogleUserModel googleUserModel)
    {
        bool isUserFound = _userManager.Users.Any(
            u => u.ExternalSignIn.Id == googleUserModel.Id);

        return isUserFound;
    }
    
    private async Task ChangeRole(UserIdentity identityUser)
    {
        await _userManager.RemoveFromRoleAsync(identityUser, Role.Guest.ToString());
        await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());
    }
}
