using System.Net;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Enums;
using Synword.ApplicationCore.Interfaces;
using Synword.Infrastructure.Identity;

namespace Synword.PublicApi.RegistrationEndpoints.GuestEndpoints;

public class GuestRegistrationEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<GuestRegistrationResponse>
{
    private readonly UserManager<AppUser>? _userManager;
    private readonly IRepository<User>? _userRepository;
    
    public GuestRegistrationEndpoint(UserManager<AppUser> userManager, IRepository<User> userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    [HttpPost("api/guestRegister")]
    public override async Task<ActionResult<GuestRegistrationResponse>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        IPAddress? ip = HttpContext.Connection.RemoteIpAddress;

        if (ip == null)
        {
            return BadRequest();
        }

        AppUser guest = new();

        guest.UserName = guest.Id;
        
        await _userManager!.CreateAsync(guest);

        await _userManager.AddToRoleAsync(guest, Role.Guest.ToString());
        
        await _userRepository!.AddAsync(
            Synword.ApplicationCore.Entities.UserAggregate.User
                .CreateDefaultGuest(guest.Id, ip.ToString()),
            cancellationToken
        );
        
        await _userRepository.SaveChangesAsync(cancellationToken);

        return Ok(new GuestRegistrationResponse(guest.Id));
    }
}
