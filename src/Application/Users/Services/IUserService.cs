using Application.Users.DTOs;

namespace Application.Users.Services;

public interface IUserService
{
    public Task<UserAuthenticateDTO> Authenticate(
        string accessToken, CancellationToken cancellationToken);
}
