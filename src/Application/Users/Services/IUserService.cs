using Synword.Application.Users.DTOs;

namespace Synword.Application.Users.Services;

public interface IUserService
{
    public Task<UserAuthenticateDto> AuthViaGoogleSignIn(
        string accessToken, 
        CancellationToken cancellationToken = default);
    public Task<UserAuthenticateDto> AuthViaEmail(
        string email, 
        string password, 
        CancellationToken cancellationToken = default);
}
