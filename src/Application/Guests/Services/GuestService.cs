using Application.Guests.DTOs;
using Application.Interfaces.Services.Token;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.Identity.Token;
using Synword.Domain.Interfaces.Services;
using Synword.Infrastructure.Identity;

namespace Application.Guests.Services;

public class GuestService : IGuestService
{
    private readonly AppIdentityDbContext _db;
    private readonly UserManager<AppUser>? _userManager;
    private readonly ITokenClaimsService? _tokenClaimsService;
    
    public GuestService(
        AppIdentityDbContext db,
        UserManager<AppUser> userManager, 
        ITokenClaimsService tokenClaimsService)
    {
        _db = db;
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<GuestAuthenticateDto> Authenticate(
        string userId, 
        CancellationToken cancellationToken)
    {
        AppUser? guest = await _userManager!.FindByIdAsync(userId);

        Guard.Against.Null(guest);
        
        string accessToken = await _tokenClaimsService!.GenerateAccessToken(guest.Id);

        RefreshToken refreshToken = await _tokenClaimsService.GenerateRefreshToken(
                guest.Id, 
                Guid.NewGuid().ToString());
        
        guest.AddRefreshToken(refreshToken);

        _db.Update(guest);
        
        await _db.SaveChangesAsync(cancellationToken);

        return new GuestAuthenticateDto(accessToken, refreshToken.Token);
    }
}
