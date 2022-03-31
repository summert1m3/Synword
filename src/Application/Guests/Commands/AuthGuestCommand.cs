using Application.Guests.DTOs;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Interfaces;
using Synword.Infrastructure.Identity;

namespace Application.Guests.Commands;

public class AuthGuestCommand : IRequest<GuestAuthenticateDTO>
{
    public AuthGuestCommand(string userId)
    {
        UserId = userId;
    }
    
    public string? UserId { get; }
}

internal class AuthGuestCommandHandler : IRequestHandler<AuthGuestCommand, GuestAuthenticateDTO>
{
    private readonly UserManager<AppUser>? _userManager;
    private readonly ITokenClaimsService? _tokenClaimsService;
    
    public AuthGuestCommandHandler(
        UserManager<AppUser> userManager, 
        ITokenClaimsService tokenClaimsService)
    {
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<GuestAuthenticateDTO> Handle(AuthGuestCommand request, CancellationToken cancellationToken)
    {
        AppUser? guest = await _userManager!.FindByIdAsync(request.UserId);

        Guard.Against.Null(guest);
        
        string token = await _tokenClaimsService!.GetTokenAsync(guest.Id);

        return new GuestAuthenticateDTO { Token = token };
    }
}
