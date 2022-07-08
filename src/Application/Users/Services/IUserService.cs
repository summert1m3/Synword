using Application.Users.DTOs;

namespace Application.Users.Services;

public interface IUserService
{
    public Task<UserAuthenticateDTO> AuthenticateViaGoogleSignIn(
        string accessToken, CancellationToken cancellationToken);
}
