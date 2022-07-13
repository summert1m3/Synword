using Synword.Application.Guests.DTOs;

namespace Synword.Application.Guests.Services;

public interface IGuestService
{
    public Task<GuestAuthenticateDto> Authenticate
        (string userId, CancellationToken cancellationToken);
}
