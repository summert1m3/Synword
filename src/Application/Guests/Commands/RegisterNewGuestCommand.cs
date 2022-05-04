using System.Net;
using Application.Guests.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Enums;
using Synword.Domain.Interfaces.Repository;
using Synword.Infrastructure.Identity;

namespace Application.Guests.Commands;

public class RegisterNewGuestCommand : IRequest<GuestRegistrationDTO>
{
    public RegisterNewGuestCommand(IPAddress ip)
    {
        Ip = ip;
    }
    public IPAddress Ip { get; }
}

internal class RegisterNewGuestCommandHandler : IRequestHandler<RegisterNewGuestCommand, GuestRegistrationDTO>
{
    private readonly UserManager<AppUser>? _userManager;
    private readonly IUserDataRepository<User>? _userRepository;

    public RegisterNewGuestCommandHandler(
        UserManager<AppUser> userManager, 
        IUserDataRepository<User> userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    public async Task<GuestRegistrationDTO> Handle(RegisterNewGuestCommand request, CancellationToken cancellationToken)
    {
        AppUser guest = new();

        guest.UserName = guest.Id;
        
        await _userManager!.CreateAsync(guest);

        await _userManager.AddToRoleAsync(guest, Role.Guest.ToString());
        
        await _userRepository!.AddAsync(
            User.CreateDefaultGuest(guest.Id, request.Ip.ToString()),
            cancellationToken
        );
        
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new GuestRegistrationDTO()
        {
            UserId = guest.Id
        };
    }
}
