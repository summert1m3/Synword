using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synword.ApplicationCore.Interfaces;
using Synword.Infrastructure.Identity;

namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GuestAuthenticateRequest>
    .WithActionResult<GuestAuthenticateResponse>
{
    private readonly UserManager<AppUser>? _userManager;
    private readonly ITokenClaimsService? _tokenClaimsService;

    public GuestAuthenticateEndpoint(UserManager<AppUser> userManager, 
        ITokenClaimsService tokenClaimsService)
    {
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
    }
    
    [HttpPost("api/guestAuthenticate")]
    public override async Task<ActionResult<GuestAuthenticateResponse>> HandleAsync(
        GuestAuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        Guid id;
        
        var isGuid = Guid.TryParse(request.UserId,out id);
        
        if (!isGuid)
        {
            throw new FormatException();
        }

        AppUser? guest = await _userManager!.FindByIdAsync(id.ToString());

        Guard.Against.Null(guest);
        
        string token = await _tokenClaimsService!.GetTokenAsync(guest.Id);
        
        return Ok(new GuestAuthenticateResponse(token));
    }
}
