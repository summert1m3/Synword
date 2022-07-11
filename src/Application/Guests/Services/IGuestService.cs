using Application.Guests.DTOs;

namespace Application.Guests.Services;

public interface IGuestService
{
    public Task<GuestAuthenticateDto> Authenticate
        (string userId, CancellationToken cancellationToken);
}
