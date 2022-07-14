using System.Net;
using MediatR;
using Synword.Application.Guests.DTOs;
using Synword.Application.Interfaces.Identity.UserIdentity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;

namespace Synword.Application.Guests.Commands;

public class RegisterNewGuestCommand : IRequest<GuestRegistrationDto>
{
    public RegisterNewGuestCommand(IPAddress ip)
    {
        Ip = ip;
    }
    public IPAddress Ip { get; }
}

internal class RegisterNewGuestCommandHandler : IRequestHandler<RegisterNewGuestCommand, GuestRegistrationDto>
{
    private readonly IUserRegistrationService _registration;
    private readonly ISynwordRepository<User>? _userRepository;
    
    public RegisterNewGuestCommandHandler(
        IUserRegistrationService registration,
        ISynwordRepository<User> userRepository)
    {
        _userRepository = userRepository;
        _registration = registration;
    }
    
    public async Task<GuestRegistrationDto> Handle(RegisterNewGuestCommand request, CancellationToken cancellationToken)
    {
        string uId = Guid.NewGuid().ToString();

        await _registration.RegisterNewGuest(uId);

        await _userRepository!.AddAsync(
            User.CreateDefaultGuest(
                uId, 
                request.Ip.ToString(),
                DateTime.Now),
            cancellationToken
        );
        
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new GuestRegistrationDto()
        {
            UserId = uId
        };
    }
}
