using Synword.Application.Guests.DTOs;
using Synword.Application.Users.DTOs;

namespace Synword.Application.Interfaces.Identity.UserIdentity;

public interface IUserAuthService
{
    public Task<GuestAuthenticateDto> GuestAuth(
        string uId, CancellationToken cancellationToken = default);
    public Task<UserAuthenticateDto> AuthViaEmail(
        string email, 
        string password, 
        CancellationToken cancellationToken = default);
    public Task<UserAuthenticateDto> AuthViaGoogle(
        string accessToken, CancellationToken cancellationToken = default);
}
