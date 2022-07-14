using Synword.Application.Interfaces.Identity.UserIdentity;
using Synword.Application.Users.DTOs;

namespace Synword.Application.Users.Services;

public class UserService : IUserService
{
    private readonly IUserAuthService _authService;

    public UserService(IUserAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<UserAuthenticateDto> AuthViaGoogleSignIn(
        string googleAccessToken, 
        CancellationToken cancellationToken = default)
    {
        return await _authService.AuthViaGoogle(googleAccessToken, cancellationToken);
    }

    public async Task<UserAuthenticateDto> AuthViaEmail(
        string email, 
        string password, 
        CancellationToken cancellationToken = default)
    {
        return await _authService.AuthViaEmail(email, password, cancellationToken);
    }
}
