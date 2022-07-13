using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Application.Guests.DTOs;
using Synword.Application.Guests.Services;

namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GuestAuthenticateRequest>
    .WithActionResult<GuestAuthenticateDto>
{
    private readonly IGuestService _guestService;
    public GuestAuthenticateEndpoint(IGuestService guestService)
    {
        _guestService = guestService;
    }
    
    [HttpPost("guestAuth")]
    [SwaggerOperation(
        Tags = new[] { "Authorization" }
    )]
    public override async Task<ActionResult<GuestAuthenticateDto>> HandleAsync(
        [FromForm]GuestAuthenticateRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var isGuid = Guid.TryParse(request.UserId,out Guid id);
        
        if (!isGuid)
        {
            return BadRequest(new FormatException());
        }

        GuestAuthenticateDto token = await _guestService
            .Authenticate(request.UserId, cancellationToken);

        return Ok(token);
    }
}
