using Application.Guests.DTOs;

namespace Application.Guests.Services;

public interface IGuestService
{
    public Task<GuestAuthenticateDTO> Authenticate
        (string userId, CancellationToken cancellationToken);
}
