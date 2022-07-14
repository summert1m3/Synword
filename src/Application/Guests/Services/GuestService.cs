using Synword.Application.Guests.DTOs;
using Synword.Application.Interfaces.Identity.UserIdentity;

namespace Synword.Application.Guests.Services;

public class GuestService : IGuestService
{
    private readonly IUserAuthService _authService;

    public GuestService(
        IUserAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<GuestAuthenticateDto> Authenticate(
        string userId, 
        CancellationToken cancellationToken)
    {
        return await _authService.GuestAuth(userId, cancellationToken);
    }
}
