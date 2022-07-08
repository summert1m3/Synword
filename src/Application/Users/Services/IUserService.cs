using Application.Users.DTOs;

namespace Application.Users.Services;

public interface IUserService
{
    public Task<UserAuthenticateDTO> AuthViaGoogleSignIn(
        string accessToken, 
        CancellationToken cancellationToken = default);
    public Task<UserAuthenticateDTO> AuthViaEmail(
        string email, 
        string password, 
        CancellationToken cancellationToken = default);
}
