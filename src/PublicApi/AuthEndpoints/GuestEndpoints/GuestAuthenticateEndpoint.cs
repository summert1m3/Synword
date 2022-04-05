using Application.Guests.Services;
using Application.Guests.DTOs;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GuestAuthenticateRequest>
    .WithActionResult<GuestAuthenticateDTO>
{
    private readonly IGuestService _guestService;
    public GuestAuthenticateEndpoint(IGuestService guestService)
    {
        _guestService = guestService;
    }
    
    [HttpPost("api/guestAuthenticate")]
    public override async Task<ActionResult<GuestAuthenticateDTO>> HandleAsync(
        GuestAuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isGuid = Guid.TryParse(request.UserId,out Guid id);
        
        if (!isGuid)
        {
            return BadRequest(new FormatException());
        }

        GuestAuthenticateDTO token = await _guestService
            .Authenticate(request.UserId, cancellationToken);

        return Ok(token);
    }
}
