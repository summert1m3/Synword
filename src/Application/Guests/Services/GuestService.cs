using Application.Guests.DTOs;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Interfaces;
using Synword.Infrastructure.Identity;

namespace Application.Guests.Services;

public class GuestService : IGuestService
{
    private readonly UserManager<AppUser>? _userManager;
    private readonly ITokenClaimsService? _tokenClaimsService;
    
    public GuestService(
        UserManager<AppUser> userManager, 
        ITokenClaimsService tokenClaimsService)
    {
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<GuestAuthenticateDTO> Authenticate(string userId, CancellationToken cancellationToken)
    {
        AppUser? guest = await _userManager!.FindByIdAsync(userId);

        Guard.Against.Null(guest);
        
        string token = await _tokenClaimsService!.GetTokenAsync(guest.Id);

        return new GuestAuthenticateDTO { Token = token };
    }
}
