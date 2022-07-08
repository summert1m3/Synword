using System.Net;
using Application.Guests.Commands;
using Application.Guests.DTOs;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.RegistrationEndpoints.GuestRegistration;

public class GuestRegistrationEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<GuestRegistrationDTO>
{
    private readonly IMediator _mediator;

    public GuestRegistrationEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("guestRegister")]
    public override async Task<ActionResult<GuestRegistrationDTO>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        IPAddress? ip = HttpContext.Connection.RemoteIpAddress;

        if (ip == null)
        {
            return BadRequest();
        }

        GuestRegistrationDTO id = 
            await _mediator.Send(
                new RegisterNewGuestCommand(ip), cancellationToken);
        
        return Ok(id);
    }
}
