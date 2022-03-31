using Application.Guests.Commands;
using Application.Guests.DTOs;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GuestAuthenticateRequest>
    .WithActionResult<GuestAuthenticateDTO>
{
    private readonly IMediator _mediator;
    public GuestAuthenticateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
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

        GuestAuthenticateDTO token = await _mediator.Send(
            new AuthGuestCommand(id.ToString()), cancellationToken);

        return Ok(token);
    }
}
