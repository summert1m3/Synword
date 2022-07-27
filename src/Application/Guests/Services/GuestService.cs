using Synword.Application.Guests.DTOs;
using Synword.Application.Interfaces.Identity.UserIdentity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;

namespace Synword.Application.Guests.Services;

public class GuestService : IGuestService
{
    private readonly IUserRegistrationService _registration;
    private readonly ISynwordRepository<User>? _userRepository;
    private readonly IUserAuthService _authService;

    public GuestService(
        IUserRegistrationService registration,
        ISynwordRepository<User> userRepository,
        IUserAuthService authService)
    {
        _userRepository = userRepository;
        _registration = registration;
        _authService = authService;
    }

    public async Task<GuestRegistrationDto> RegisterNewGuest(
        string ip,
        CancellationToken cancellationToken = default)
    {
        string uId = Guid.NewGuid().ToString();

        await _registration.RegisterNewGuest(uId);

        await _userRepository!.AddAsync(
            User.CreateDefaultGuest(
                uId, 
                ip,
                DateTime.Now),
            cancellationToken
        );
        
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new GuestRegistrationDto()
        {
            UserId = uId
        };
    }

    public async Task<GuestAuthenticateDto> Authenticate(
        string userId, 
        CancellationToken cancellationToken)
    {
        return await _authService.GuestAuth(userId, cancellationToken);
    }
}
