using System.Net;
using Application.Guests.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Enums;
using Synword.Domain.Interfaces.Repository;
using Synword.Infrastructure.Identity;

namespace Application.Guests.Commands;

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
    private readonly UserManager<AppUser>? _userManager;
    private readonly ISynwordRepository<User>? _userRepository;

    public RegisterNewGuestCommandHandler(
        UserManager<AppUser> userManager, 
        ISynwordRepository<User> userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    public async Task<GuestRegistrationDto> Handle(RegisterNewGuestCommand request, CancellationToken cancellationToken)
    {
        AppUser guest = new();

        guest.UserName = guest.Id;
        
        await _userManager!.CreateAsync(guest);

        await _userManager.AddToRoleAsync(guest, Role.Guest.ToString());
        
        await _userRepository!.AddAsync(
            User.CreateDefaultGuest(
                guest.Id, 
                request.Ip.ToString(),
                DateTime.Now),
            cancellationToken
        );
        
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new GuestRegistrationDto()
        {
            UserId = guest.Id
        };
    }
}
