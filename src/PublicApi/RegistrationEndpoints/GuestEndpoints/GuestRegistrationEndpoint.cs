using System.Net;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint;
using Synword.ApplicationCore.Entities.UserAggregate;
using Synword.ApplicationCore.Entities.UserAggregate.ValueObjects;
using Synword.ApplicationCore.Interfaces;
using Synword.Infrastructure.Identity;
using Synword.PublicApi.RegistrationEndpoints.Guest;

namespace Synword.PublicApi.RegistrationEndpoints.GuestEndpoints;

public class GuestRegistrationEndpoint : IEndpoint<IResult>
{
    private UserManager<AppUser>? _userManager;
    private IRepository<User>? _userRepository;
    private HttpContext? _context;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/guestRegister", 
                async(UserManager < AppUser > userManager, IRepository<User> userRepository, 
                    HttpContext context) =>
            {
                _userManager = userManager;
                _userRepository = userRepository;
                _context = context;
                return await HandleAsync();
            })
            .Produces<GuestRegistrationResponse>();
    }
    
    public async Task<IResult> HandleAsync()
    {
        Guard.Against.Null(_context, nameof(_context));
        Guard.Against.Null(_userManager, nameof(_userManager));
        Guard.Against.Null(_userRepository, nameof(_userRepository));
        
        IPAddress? ip = _context.Connection.RemoteIpAddress;

        if (ip == null)
        {
            return Results.BadRequest();
        }

        AppUser guest = new();

        guest.UserName = guest.Id;
        
        await _userManager.CreateAsync(guest);

        await _userManager.AddToRoleAsync(guest, Role.Guest.ToString());
        
        await _userRepository.AddAsync(
            User.CreateDefaultGuest(guest.Id, ip.ToString())
        );
        
        await _userRepository.SaveChangesAsync();

        return Results.Ok(new GuestRegistrationResponse(guest.Id));
    }
}
